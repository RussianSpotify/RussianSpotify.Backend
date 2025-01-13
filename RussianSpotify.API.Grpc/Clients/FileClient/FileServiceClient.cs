#region

using RussianSpotify.API.Shared.Exceptions.FileExceptions;

#endregion

namespace RussianSpotify.API.Grpc.Clients.FileClient;

public class FileServiceClient : IFileServiceClient
{
    private const string ImageFileStartsWith = "image/";
    private const string AudioFileStartsWith = "audio/";

    private readonly FileService.FileServiceClient _fileClient;

    public FileServiceClient(FileService.FileServiceClient fileClient)
    {
        _fileClient = fileClient;
    }

    /// <inheritdoc />
    public bool IsImage(string сontentType)
    {
        if (сontentType is null)
            throw new FileInternalException("File's content type not set");

        return сontentType.StartsWith(ImageFileStartsWith);
    }

    /// <inheritdoc />
    public bool IsAudio(string contentType)
    {
        if (contentType is null)
            throw new FileInternalException("File's content type not set");

        return contentType.StartsWith(AudioFileStartsWith);
    }

    public async Task<Models.File> GetFileAsync(Guid? fileId, CancellationToken cancellationToken = default)
    {
        return (await GetFilesAsync(new[] { fileId }, cancellationToken)).First();
    }

    public async Task<Models.FileMetadata> GetFileMetadataAsync(Guid? fileId,
        CancellationToken cancellationToken = default)
    {
        return (await GetFilesMetadataAsync(new[] { fileId }, cancellationToken)).First();
    }

    public async Task<ICollection<Models.FileMetadata>> GetFilesMetadataAsync(IReadOnlyCollection<Guid?> ids,
        CancellationToken cancellationToken = default)
    {
        if (ids.Any(id => id == null))
            throw new FileInternalException("Ids cannot be null");

        var request = new GetFilesRequest { FilesIds = { ids.Select(id => id!.Value.ToString()) } };
        var response = await _fileClient.GetFilesMetadataAsync(request, cancellationToken: cancellationToken);

        var metadata = response.FilesMetadata
            .Select(x => new Models.FileMetadata
            {
                Id = Guid.Parse(x.Id),
                Address = x.Address,
                ContentType = x.ContentType,
                FileName = x.FileName,
                UserId = Guid.Parse(x.UserId)
            });

        return metadata.ToList();
    }

    public static Models.FileMetadata MapFromGrpcModel(FileMetadata metadata)
        => new()
        {
            Id = Guid.Parse(metadata.Id),
            Address = metadata.Address,
            ContentType = metadata.ContentType,
            FileName = metadata.FileName,
            UserId = Guid.Parse(metadata.UserId)
        };


    public async Task<ICollection<Models.File>> GetFilesAsync(IReadOnlyCollection<Guid?> ids,
        CancellationToken cancellationToken = default)
    {
        if (ids.Any(id => id == null))
            throw new FileInternalException("Ids cannot be null");

        var request = new GetFilesRequest { FilesIds = { ids.Select(id => id!.Value.ToString()) } };
        var response = await _fileClient.GetFilesAsync(request, cancellationToken: cancellationToken);

        var result = new List<Models.File>();
        foreach (var item in response.Files)
        {
            await using var content = new MemoryStream(item.Content.ToByteArray());
            result.Add(new Models.File
            {
                Content = content,
                Metadata = MapFromGrpcModel(item.FileMetadata)
            });
        }

        return result;
    }

    public async Task DeleteAsync(Guid? fileId, CancellationToken cancellationToken = default)
    {
        await DeleteAsync(new[] { fileId }, cancellationToken);
    }

    public async Task DeleteAsync(IReadOnlyCollection<Guid?> filesIds, CancellationToken cancellationToken = default)
    {
        if (filesIds.Any(id => id == null))
            throw new FileInternalException("Ids cannot be null");

        var request = new DeleteFilesRequest { FilesIds = { filesIds.Select(id => id!.Value.ToString()) } };
        await _fileClient.DeleteFilesAsync(request, cancellationToken: cancellationToken);
    }
}