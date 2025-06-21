using Microsoft.FeatureManagement;

namespace App.Components.Pages.Home.Components;

public sealed partial class BlogSection
{
    private readonly IFeatureManager _featureManager;

    private bool IsEnabled { get; set; }
    
    public BlogSection(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    protected override async Task OnInitializedAsync()
    {
        IsEnabled = await _featureManager.IsEnabledAsync(FeatureFlags.Blog);
    }
}