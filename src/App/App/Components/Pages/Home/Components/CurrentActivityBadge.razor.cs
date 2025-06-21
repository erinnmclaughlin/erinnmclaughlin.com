using App.Integrations.Discord;
using Microsoft.AspNetCore.Components;

namespace App.Components.Pages.Home.Components;

public partial class CurrentActivityBadge : ComponentBase
{
    private readonly DiscordApiClient _discordApi;

    private DiscordUserData? UserData { get; set; }
    
    public CurrentActivityBadge(DiscordApiClient discordApi)
    {
        _discordApi = discordApi;
    }
    
    protected override async Task OnInitializedAsync()
    {
        UserData = await _discordApi.GetUserDetail();
    }
}