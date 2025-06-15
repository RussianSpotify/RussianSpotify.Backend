using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Data.Cassandra;
using RussianSpotify.API.Shared.Exceptions;
using RussianSpotify.API.Shared.Models.SongEvents;

namespace RussianSpotify.API.Core.Requests.Music.UpdateSongPlaysNumber;

public class UpdateSongPlaysNumberCommandHandler : IRequestHandler<UpdateSongPlaysNumberCommand>
{
    private readonly IDbContext _dbContext;

    private readonly CassandraContext _cassandraContext;

    private readonly IBus _bus;

    public UpdateSongPlaysNumberCommandHandler(
        IDbContext dbContext,
        ILogger<UpdateSongPlaysNumberCommandHandler> logger,
        IBus bus,
        CassandraContext cassandraContext)
    {
        _dbContext = dbContext;
        _bus = bus;
        _cassandraContext = cassandraContext;
    }

    public async Task Handle(UpdateSongPlaysNumberCommand request, CancellationToken cancellationToken)
    {
        var songStatistic =
            await _cassandraContext.FindByAsync<SongStatistic>(nameof(SongStatistic.Id), request.SongId);

        if (songStatistic.Count == 0)
        {
            var guid = Guid.Parse(request.SongId);
            var hasSongWithSameId = await _dbContext.Songs.AnyAsync(x => x.Id == guid, cancellationToken);

            if (!hasSongWithSameId)
                throw new NotFoundException($"Song with id {request.SongId} was not found");
            
            await _cassandraContext.InsertAsync(new SongStatistic
            {
                PlaysNumber = 1,
                Id = request.SongId
            });
        }
        else
        {
            songStatistic.First().PlaysNumber += 1;
            await _cassandraContext.UpdateAsync(songStatistic.First());   
        }
        
        await _bus.Publish(new PlaysNumberUpdatedEvent
        {
            SongId = request.SongId,
            CurrentPlaysNumber = (uint)songStatistic.First().PlaysNumber
        }, cancellationToken);
    }
}