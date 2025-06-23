var builder = DistributedApplication.CreateBuilder(args);

var customDomain = builder.AddParameter("custom-domain");
var certificateName = builder.AddParameter("certificate-name");

var githubAccessToken = builder.AddParameter("github-access-token", secret: true);

var steamApiKey =  builder.AddParameter("steam-api-key", secret: true);
var steamUserId =  builder.AddParameter("steam-id");

var postgresUserName = builder.AddParameter("postgres-username");
var postgresPassword = builder.AddParameter("postgres-password", secret: true);
var postgres = builder.AddPostgres("postgres", postgresUserName, postgresPassword);

var blogDb = postgres.AddDatabase("blogdb")
    .WithCreationScript("create database blogdb");

var blogDbMigrator = builder
    .AddProject<Projects.Blog_DbMigrator>("blogdbmigrator")
    .WithReference(blogDb)
    .WaitFor(blogDb);

builder
    .AddProject<Projects.Blog_AdminPortal>("blogadminportal")
    .WithReference(blogDb)
    .WaitForCompletion(blogDbMigrator)
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.App>("app")
    .WithEnvironment("github-access-token", githubAccessToken)
    .WithEnvironment("steam-api-key", steamApiKey)
    .WithEnvironment("steam-id", steamUserId)
    .WithReference(blogDb)
    .WaitForCompletion(blogDbMigrator)
    .WithExternalHttpEndpoints()
    .PublishAsAzureContainerApp((_, app) =>
    {
#pragma warning disable ASPIREACADOMAINS001
        // this feature is currently experimental
        app.ConfigureCustomDomain(customDomain, certificateName);
#pragma warning restore ASPIREACADOMAINS001
    });

builder.Build().Run();
