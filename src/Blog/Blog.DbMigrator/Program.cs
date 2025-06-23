using Blog.DbMigrator;
using FluentMigrator.Runner;
using Npgsql;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("blogdb");

builder.Services
    .AddFluentMigratorCore()
    .ConfigureRunner(rb =>
    {
        rb.AddPostgres().WithGlobalConnectionString(connectionString);
        rb.ScanIn(typeof(Program).Assembly).For.All();
    })
    .AddLogging(lb => lb.AddFluentMigratorConsole());

builder.Services.AddScoped<BlogDbMigrator>();

var app = builder.Build();

using var scope = app.Services.CreateScope();

// apply migrations
var migrator = scope.ServiceProvider.GetRequiredService<BlogDbMigrator>();
migrator.Run();

// apply seed data
if (builder.Environment.IsDevelopment())
{
    await using var connection = new NpgsqlConnection(connectionString);
    await connection.OpenAsync();
    await BlogDbSeeder.SeedAsync(connection);
}
