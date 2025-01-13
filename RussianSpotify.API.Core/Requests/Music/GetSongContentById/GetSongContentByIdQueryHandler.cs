#region

using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Grpc.Clients.FileClient;
using RussianSpotify.Contracts.Requests.Music.GetSongContentById;

#endregion

namespace RussianSpotify.API.Core.Requests.Music.GetSongContentById;

/// <summary>
///     Обработчик для <see cref="GetSongContentByIdQuery" />
/// </summary>
public class GetSongContentByIdQueryHandler : IRequestHandler<GetSongContentByIdQuery, GetSongContentByIdResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IFileServiceClient _fileServiceClient;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="fileServiceClient">Сервис S3</param>
    public GetSongContentByIdQueryHandler(
        IDbContext dbContext,
        IFileServiceClient fileServiceClient)
    {
        _dbContext = dbContext;
        _fileServiceClient = fileServiceClient;
    }

    /// <inheritdoc />
    public async Task<GetSongContentByIdResponse> Handle(
        GetSongContentByIdQuery request,
        CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var songFromDb = await _dbContext.Songs
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new EntityNotFoundException<Song>(request.Id);

        if(songFromDb.SongFileId == null)
            throw new EntityNotFoundException<Song>(request.Id);

        var songFromS3 = await _fileServiceClient.GetFileAsync(
            songFromDb.SongFileId.Value,
            cancellationToken: cancellationToken)
        ?? throw new EntityNotFoundException<Song>(songFromDb.SongFileId.Value);

        return new GetSongContentByIdResponse(
            songFromS3.Content,
            songFromS3.Metadata.FileName,
            songFromS3.Metadata.ContentType);
    }
}