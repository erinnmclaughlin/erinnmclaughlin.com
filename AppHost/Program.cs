var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.App>("app");

builder.Build().Run();
