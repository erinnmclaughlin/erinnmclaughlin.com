using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

namespace App.Integrations.GitHub;

public sealed class GitHubAuthenticationHandler : DelegatingHandler
{
    private readonly GitHubOptions _options;

    public GitHubAuthenticationHandler(IOptions<GitHubOptions> options)
    {
        _options = options.Value;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_options.AccessToken is { } accessToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
        
        return base.SendAsync(request, cancellationToken);
    }
}