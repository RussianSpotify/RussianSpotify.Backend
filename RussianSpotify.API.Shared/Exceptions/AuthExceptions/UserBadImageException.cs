#region

#endregion

namespace RussianSpotify.API.Shared.Exceptions.AuthExceptions;

public class UserBadImageException : BadRequestException
{
    public UserBadImageException(string message) : base(message)
    {
    }
}