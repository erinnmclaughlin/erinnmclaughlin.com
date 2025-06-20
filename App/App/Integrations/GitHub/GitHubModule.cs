namespace App.Integrations.GitHub;

public static class GitHubModule
{
    public static T AddGitHubApiClient<T>(this T builder) where T : IHostApplicationBuilder
    {
        builder.Services.Configure<GitHubOptions>(o =>
        {
            o.AccessToken = builder.Configuration["github-access-token"];
        });
        
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