using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Shared.Exceptions;
using RussianSpotify.API.Shared.Models.SongEvents;

namespace RussianSpotify.API.Core.Requests.Music.UpdateSongPlaysNumber;

public class UpdateSongPlaysNumberCommandHandler : IRequestHandler<UpdateSongPlaysNumberCommand>
{
    private readonly IDbContext _dbContext;
    
    private readonly IBus _bus;

    public UpdateSongPlaysNumberCommandHandler(
        IDbContext dbContext,
        ILogger<UpdateSongPlaysNumberCommandHandler> logger,
        IBus bus)
    {
        _dbContext = dbContext;
        _bus = bus;
    }

    public async Task Handle(UpdateSongPlaysNumberCommand request, CancellationToken cancellationToken)
    {
        var song = await _dbContext.Songs.FirstOrDefaultAsync(x => x.Id == request.SongId, cancellationToken);

        if (song == null)
            throw new NotFoundException($"Song with id {request.SongId} was not found");

        song.PlaysNumber += 1;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _bus.Publish(new PlaysNumberUpdatedEvent
        {
            SongId = request.SongId,
            CurrentPlaysNumber = song.PlaysNumber
        }, cancellationToken);
    }
}