using App.Integrations.Discord;

namespace App.Components.Pages.Home.Components;

public interface IAppCurrentActivityBadgeViewModel
{
    Task<CurrentActivity?> GetCurrentActivityAsync(CancellationToken cancellationToken = default);
}

public sealed class AppCurrentActivityBadgeViewModel : IAppCurrentActivityBadgeViewModel
{
    private readonly DiscordApiClient _discordApi;
    
    public AppCurrentActivityBadgeViewModel(DiscordApiClient discordApi)
    {
        _discordApi = discordApi;
    }
    
    public async Task<CurrentActivity?> GetCurrentActivityAsync(CancellationToken cancellationToken = default)
    {
        var userDetail = await _discordApi.GetUserDetail(cancellationToken);

        if (userDetail?.Activities.FirstOrDefault(x => x.Type == DiscordActivityType.Playing) is { } playingActivity)
        {
            return new CurrentActivity
            {
                Action = playingActivity.Name,
                ActionLink = null,
                Verb = playingActivity.Name is "Rider" ? "programming in" : "playing"
            };
        }

        if (userDetail?.CurrentSpotifyTrack is { } listeningActivity)
        {
            return new CurrentActivity
            {
                Action = listeningActivity.Artist,
                ActionLink = $"https://open.spotify.com/track/{listeningActivity.TrackId}",
                Verb = "listening to"
            };
        }

        return null;
    }
}