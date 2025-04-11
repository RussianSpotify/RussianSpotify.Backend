#region

using Hangfire.Dashboard;

#endregion

namespace RussianSpotify.API.Shared.Options;

public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    /// <inheritdoc />
    public bool Authorize(DashboardContext context) => true;
}