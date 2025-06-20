using App.Features;

namespace App.Components.Pages.Home;

public sealed partial class HomePage
{
    private readonly IFeaturedProjectsProvider _featuredProjectsProvider;
    private FeaturedProjectDefinition[]? FeaturedProjects { get; set; }

    public HomePage(IFeaturedProjectsProvider featuredProjectsProvider)
    {
        _featuredProjectsProvider = featuredProjectsProvider;
    }

    protected override async Task OnInitializedAsync()
    {
        FeaturedProjects = await _featuredProjectsProvider.GetFeaturedProjectsAsync();
    }
}
