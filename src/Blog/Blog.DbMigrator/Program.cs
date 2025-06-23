using Blog.DbMigrator;
using FluentMigrator.Runner;

var builder = Host.CreateApplicationBuilder(args);

builder.AddNpgsqlDataSource("blogdb");

builder.Services
    .AddFluentMigratorCore()
    .ConfigureRunner(rb =>
    {
        rb.AddPostgres().WithGlobalConnectionString(builder.Configuration.GetConnectionString("blogdb"));
        rb.ScanIn(typeof(Program).Assembly).For.All();
    })
    .AddLogging(lb => lb.AddFluentMigratorConsole());

builder.Services.AddScoped<BlogDbMigrator>();
builder.Services.AddScoped<BlogDbSeeder>();

var app = builder.Build();

using var scope = app.Services.CreateScope();

// Apply Migrations
var migrator = scope.ServiceProvider.GetRequiredService<BlogDbMigrator>();
migrator.Run();

// Apply Seed Data
if (builder.Environment.IsDevelopment())
{
    var seeder = scope.ServiceProvider.GetRequiredService<BlogDbSeeder>();
    await seeder.SeedAsync();
}
