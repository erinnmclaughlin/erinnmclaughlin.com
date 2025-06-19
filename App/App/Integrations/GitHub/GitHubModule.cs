namespace App.Integrations.GitHub;

public static class GitHubModule
{
    public const string ConfigurationSectionName = "GitHub";
    
    public static T AddGitHubApiClient<T>(this T builder, string configurationSectionName = ConfigurationSectionName)
        where T : IHostApplicationBuilder
    {
        builder.Services.Configure<GitHubOptions>(builder.Configuration.GetSection(configurationSectionName));
        builder.Services.AddTransient<GitHubAuthenticationHandler>();
        
        var apiClientBuilder = builder.Services.AddHttpClient<IGitHubApiClient, GitHubApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.github.com");
            client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("erinnmclaughlin.com");
        });
        
        apiClientBuilder.AddHttpMessageHandler<GitHubAuthenticationHandler>();
        
        return builder;
    }
}