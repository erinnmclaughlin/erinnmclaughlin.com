using FluentMigrator.Runner;

var builder = Host.CreateApplicationBuilder(args);

builder.AddNpgsqlDataSource("blogdb");

builder.Services
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb.AddPostgres().ScanIn(typeof(Program).Assembly).For.All())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

var host = builder.Build();

using var scope = host.Services.CreateScope();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

if (runner.HasMigrationsToApplyUp())
{
    logger.LogInformation("Applying migrations...");
    
    try
    {
        runner.MigrateUp();
        logger.LogInformation("Migrations successfully applied.");
    }
    catch (Exception ex)
    { 
        logger.LogError(ex, "Error while migrating up");
        throw;
    }
}
else
{
    logger.LogInformation("No migrations to apply.");
}
