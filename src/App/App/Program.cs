using App.Components;
using App.Components.Pages.Home.Components;
using App.Integrations.Discord;
using App.Integrations.GitHub;
using App.Integrations.NuGet;
using App.Integrations.Steam;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDataSource("blogdb");
builder.Services.AddScoped<IAppBlogSectionViewModel, AppBlogSectionViewModel>();

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddTransient<IAppBlogSectionViewModel, AppBlogSectionViewModel>();
builder.Services.AddTransient<IAppCurrentActivityBadgeViewModel, AppCurrentActivityBadgeViewModel>();
builder.Services.AddTransient<IAppProjectsSectionViewModel, AppProjectsSectionViewModel>();
builder.Services.AddTransient<IAppProjectCardViewModel, AppProjectCardViewModel>();

builder.Services.AddFeatureManagement();

builder
    .AddGitHubApiClient()
    .AddSteamApiClient();

builder.Services.AddScoped<INuGetApiClient, NuGetApiClient>();
builder.Services.AddHttpClient<DiscordApiClient>(client => client.BaseAddress = new Uri("https://api.lanyard.rest/v1/"));

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<Main>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(App.Client._Imports).Assembly);

app.Run();
