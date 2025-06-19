namespace App.Integrations.GitHub;

public sealed record GitHubOptions
{
    public string? AccessToken { get; init; }
    public string Username { get; init; } = "erinnmclaughlin";
}
