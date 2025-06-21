using App.Features;
using Microsoft.AspNetCore.Components;

namespace App.Components.Pages.Home.Components;

public partial class ProjectCard
{
    private readonly IFeaturedProjectsProvider _featuredProjectsProvider;
    
    [Parameter] 
    public required FeaturedProjectDefinition ProjectDefinition { get; set; }

    public FeaturedProject? Project { get; private set; }

    public ProjectCard(IFeaturedProjectsProvider featuredProjectsProvider)
    {
        _featuredProjectsProvider = featuredProjectsProvider;
    }
    
    protected override async Task OnInitializedAsync()
    {
        Project = await _featuredProjectsProvider.GetFeaturedProjectDetailsAsync(ProjectDefinition);
    }
    
    private static string GetLanguageColor(string language) => language switch
    {
        "C#" => "#178600",
        "CSS" => "#663399",
        "HTML" => "#e34c26",
        "JavaScript" => "#f1e05a",
        _ => "var(--dark)"
    };
}