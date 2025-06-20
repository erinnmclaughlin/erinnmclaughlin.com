var builder = DistributedApplication.CreateBuilder(args);

var githubAccessToken = builder.AddParameter("github-access-token", secret: true);

var steamApiKey =  builder.AddParameter("steam-api-key", secret: true);
var steamUserId =  builder.AddParameter("steam-id");

builder.AddProject<Projects.App>("app")
    .WithEnvironment("GitHub:AccessToken", githubAccessToken)
    .WithEnvironment("Steam:ApiKey", steamApiKey)
    .WithEnvironment("Steam:SteamId", steamUserId);

builder.Build().Run();
