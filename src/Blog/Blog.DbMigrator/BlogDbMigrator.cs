using FluentMigrator.Runner;

namespace Blog.DbMigrator;

public sealed class BlogDbMigrator
{
    private readonly ILogger<BlogDbMigrator> _logger;
    private readonly IMigrationRunner _runner;

    public BlogDbMigrator(ILogger<BlogDbMigrator> logger, IMigrationRunner runner)
    {
        _logger = logger;
        _runner = runner;
    }
    
    public void Run()
    {
        if (!_runner.HasMigrationsToApplyUp())
        {
            _logger.LogInformation("No migrations to apply.");
            return;
        }
        
        _logger.LogInformation("Applying migrations...");
        
        try
        {
            _runner.MigrateUp();
        }
        catch (Exception ex)
        { 
            _logger.LogError(ex, "Error while migrating up");
            throw;
        }
        
        _logger.LogInformation("Migrations successfully applied.");
    }
}
