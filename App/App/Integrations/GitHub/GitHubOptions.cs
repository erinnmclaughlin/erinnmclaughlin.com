namespace App.Integrations.GitHub;

public sealed record GitHubOptions
{
    public string? AccessToken { get; set; }
    public string Username { get; set; } = "erinnmclaughlin";
}
