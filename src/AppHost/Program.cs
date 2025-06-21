var builder = DistributedApplication.CreateBuilder(args);

var customDomain = builder.AddParameter("custom-domain");
var certificateName = builder.AddParameter("certificate-name");

var githubAccessToken = builder.AddParameter("github-access-token", secret: true);

var steamApiKey =  builder.AddParameter("steam-api-key", secret: true);
var steamUserId =  builder.AddParameter("steam-id");

builder.AddProject<Projects.App>("app")
    .WithEnvironment("github-access-token", githubAccessToken)
    .WithEnvironment("steam-api-key", steamApiKey)
    .WithEnvironment("steam-id", steamUserId)
    .WithExternalHttpEndpoints()
    .PublishAsAzureContainerApp((_, app) =>
    {
#pragma warning disable ASPIREACADOMAINS001
        // this feature is currently experimental
        app.ConfigureCustomDomain(customDomain, certificateName);
#pragma warning restore ASPIREACADOMAINS001
    });

builder.Build().Run();
