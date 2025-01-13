#region

using Hangfire.Dashboard;

#endregion

namespace RussianSpotify.API.Worker;

public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    /// <inheritdoc />
    public bool Authorize(DashboardContext context) => true;
}