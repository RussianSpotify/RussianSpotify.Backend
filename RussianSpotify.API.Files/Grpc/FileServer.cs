using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Files.Data;
using RussianSpotify.API.Files.Interfaces;
using RussianSpotify.API.Grpc;
using File = RussianSpotify.API.Grpc.File;

namespace RussianSpotify.API.Files.Grpc;

public class FileServer : FileService.FileServiceBase
{
    private readonly IS3Service _s3Service;

    private readonly IDbContext _dbContext;

    private readonly ILogger<FileServer> _logger;

    public FileServer(IS3Service s3Service, IDbContext dbContext, ILogger<FileServer> logger)
    {
        _s3Service = s3Service;
        _dbContext = dbContext;
        _logger = logger;
    }

    private Guid[] GetFilesIds(RepeatedField<string> ids)
    {
        var filesIds = new Guid[ids.Count];

        for (var i = 0; i < ids.Count; i++)
        {
            var isCorrectId = Guid.TryParse(ids[i], out var fileId);
            filesIds[i] = fileId;

            if (!isCorrectId)
            {
                _logger.LogError("Invalid file id {id}", ids[i]);
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    $"Invalid file id {ids[i]}"));
            }
        }

        return filesIds;
    }

    public override async Task<GetFilesMetadataResponse> GetFilesMetadata(GetFilesRequest request,
        ServerCallContext context)
    {
        var filesIds = GetFilesIds(request.FilesIds);

        var filesMetadata =
            await _dbContext.FilesMetadata
                .AsNoTracking()
                .Where(x => filesIds.Contains(x.Id))
                .Select(x => new
                {
                    x.Id,
                    x.ContentType,
                    x.UserId,
                    x.FileName,
                    x.Address,
                })
                .ToListAsync(context.CancellationToken);

        if (filesMetadata.Count == request.FilesIds.Count)
        {
            _logger.LogError("FilesMetadata with ids: {fileId} not found in database",
                string.Join(", ",
                    filesMetadata
                        .Select(x => x.Id)
                        .Where(x => !request.FilesIds.Contains(x.ToString()))));
            throw new RpcException(new Status(StatusCode.NotFound,
                $"FileMetadata with id {string.Join(", ", request.FilesIds)} not found in database"));
        }

        return new GetFilesMetadataResponse
        {
            FilesMetadata =
            {
                filesMetadata.Select(x => new FileMetadata
                {
                    Id = x.Id.ToString(),
                    ContentType = x.ContentType,
                    FileName = x.FileName,
                    UserId = x.UserId.ToString(),
                    Address = x.Address
                })
            }
        };
    }

    public override async Task<GetFilesResponse> GetFiles(GetFilesRequest request, ServerCallContext context)
    {
        var filesMetadata = (await GetFilesMetadata(request, context)).FilesMetadata;

        var responseItems = new List<File>(filesMetadata.Count);
        foreach (var fileMetadata in filesMetadata)
        {
            // TODO: Написать метод для получения множества файлов в сервисе IS3Service, а то IO Bound на том, что каждый раз спрашиваем, существует ли бакет или нет
            var file = await _s3Service.GetFileAsync(fileMetadata.Address,
                cancellationToken: context.CancellationToken);

            if (file == null)
            {
                _logger.LogError("File with address {fileAddress} not found", fileMetadata.Address);
                throw new RpcException(new Status(StatusCode.NotFound,
                    $"FileMetadata with id {fileMetadata.Address} not found in S3"));
            }

            using var memoryStream = new MemoryStream();
            await file.Content.CopyToAsync(memoryStream);
            var byteString = ByteString.CopyFrom(memoryStream.ToArray());

            responseItems.Add(new File
            {
                FileMetadata = fileMetadata,
                Content = byteString
            });
        }

        return new GetFilesResponse
        {
            Files = { responseItems }
        };
    }

    public override async Task<Empty> DeleteFiles(DeleteFilesRequest request, ServerCallContext context)
    {
        try
        {
            await using var dbContextTransaction =
                await _dbContext.Database.BeginTransactionAsync(context.CancellationToken);

            var filesIds = GetFilesIds(request.FilesIds);

            var files = await _dbContext.FilesMetadata
                .Where(x => filesIds.Contains(x.Id))
                .ToArrayAsync(context.CancellationToken);

            _dbContext.FilesMetadata.RemoveRange(files);

            // TODO: Написать метод для удаления множества файлов в сервисе IS3Service, а то IO Bound на том, что каждый раз спрашиваем, существует ли бакет или нет
            foreach (var file in files)
                await _s3Service.DeleteAsync(file.Address, cancellationToken: context.CancellationToken);
            
            _logger.LogInformation("Deleted files: {filesId}", string.Join(", ", files.Select(x => x.Id)));
            await _dbContext.SaveChangesAsync(context.CancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting files with ids: {filesIds}", string.Join(", ", request.FilesIds));
            throw;
        }

        return new Empty();
    }
}