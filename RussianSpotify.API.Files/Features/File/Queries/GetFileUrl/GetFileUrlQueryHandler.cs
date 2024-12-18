using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Files.Data;
using RussianSpotify.API.Files.Domain.Entities;
using RussianSpotify.API.Files.Exceptions;
using RussianSpotify.API.Files.Interfaces;
using RussianSpotify.API.Files.Requests.File.GetFileUrl;

namespace RussianSpotify.API.Files.Features.File.Queries.GetFileUrl;

/// <summary>
/// Обработчик для <see cref="GetFileUrlQuery"/>
/// </summary>
public class GetFileUrlQueryHandler : IRequestHandler<GetFileUrlQuery, GetFileUrlResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IS3Service _s3Service;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="s3Service">Сервис S3</param>
    /// <param name="dbContext">Контекст БД</param>
    public GetFileUrlQueryHandler(IS3Service s3Service, IDbContext dbContext)
    {
        _s3Service = s3Service;
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<GetFileUrlResponse> Handle(GetFileUrlQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var file = await _dbContext.FilesMetadata
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new EntityNotFoundException<FileMetadata>(request.Id);

        var url = await _s3Service.GetFileUrlAsync(file.Address, cancellationToken: cancellationToken);

        return new GetFileUrlResponse
        {
            FileUrl = url,
        };
    }
}