using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Data.Cassandra;
using RussianSpotify.API.Shared.Exceptions;

namespace RussianSpotify.API.Core.Requests.Music.GetSongPlaysNumber;

public class GetSongPlaysNumberQueryHandler : IRequestHandler<GetSongPlaysNumberQuery, int>
{
    private readonly CassandraContext _cassandraContext;
    
    private readonly IDbContext _dbContext;
    
    public GetSongPlaysNumberQueryHandler(CassandraContext cassandraContext, IDbContext dbContext)
    {
        _cassandraContext = cassandraContext;
        _dbContext = dbContext;
    }
    
    public async Task<int> Handle(GetSongPlaysNumberQuery request, CancellationToken cancellationToken)
    {
        var songStatistic =
            await _cassandraContext.FindByAsync<SongStatistic>(nameof(SongStatistic.Id), request.SongId);

        if (songStatistic.Count == 0)
        {
            var guid = Guid.Parse(request.SongId);
            var hasSongWithSameId = await _dbContext.Songs.AnyAsync(x => x.Id == guid, cancellationToken);
            
            if (!hasSongWithSameId)
                throw new NotFoundException($"Song with id {request.SongId} was not found");
            
            return 0;
        }
        
        return songStatistic.First().PlaysNumber;
    }
}