using Microsoft.FeatureManagement;

namespace App.Components.Pages.Home;

public partial class HomePage
{
    private readonly IFeatureManager _featureManager;

    public bool ShowBlogSection { get; set; }
    
    public HomePage(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    protected override async Task OnInitializedAsync()
    {
        ShowBlogSection = await _featureManager.IsEnabledAsync(FeatureFlags.Blog);
    }
}