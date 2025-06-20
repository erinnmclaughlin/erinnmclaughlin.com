using App.Features;
using Microsoft.AspNetCore.Components;

namespace App.Components.Pages.Home.Components;

public sealed partial class FeaturedProjectsGrid : ComponentBase
{
    private readonly IFeaturedProjectsProvider _featuredProjectsProvider;
    
    private FeaturedProjectDefinition[]? FeaturedProjects { get; set; }

    public FeaturedProjectsGrid(IFeaturedProjectsProvider featuredProjectsProvider)
    {
        _featuredProjectsProvider = featuredProjectsProvider;
    }

    protected override async Task OnInitializedAsync()
    {
        FeaturedProjects = await _featuredProjectsProvider.GetFeaturedProjectsAsync();
    }
}