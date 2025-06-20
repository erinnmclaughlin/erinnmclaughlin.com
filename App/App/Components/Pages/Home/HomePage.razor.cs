using App.Features;
using App.Integrations.Discord;

namespace App.Components.Pages.Home;

public sealed partial class HomePage
{
    private readonly DiscordApiClient _discordApi;
    private readonly IFeaturedProjectsProvider _featuredProjectsProvider;
    
    private DiscordUserData? DiscordUser { get; set; }
    private FeaturedProjectDefinition[]? FeaturedProjects { get; set; }

    public HomePage(IFeaturedProjectsProvider featuredProjectsProvider, DiscordApiClient discordApi)
    {
        _featuredProjectsProvider = featuredProjectsProvider;
        _discordApi = discordApi;
    }

    protected override async Task OnInitializedAsync()
    {
        DiscordUser = await _discordApi.GetUserDetail();
        FeaturedProjects = await _featuredProjectsProvider.GetFeaturedProjectsAsync();
    }
}
