using Dapper;
using Npgsql;

namespace Blog.DbMigrator;

public sealed class BlogDbSeeder
{
    private readonly NpgsqlDataSource _dataSource;
    
    public BlogDbSeeder(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        if (await connection.ExecuteScalarAsync<bool>("SELECT EXISTS(SELECT 1 FROM posts);"))
            return;

        var sql =
            """
            insert into posts (id, slug, title, content_preview, content, created_at)
            values
            (
              gen_random_uuid(), 
             'first-post', 
             'First Post', 
             'This is the first post preview.', 
             'This is the content of the first post.', 
             now()
             );
            """;

        var command = new CommandDefinition(sql, cancellationToken: cancellationToken);
        await connection.ExecuteAsync(command);
    }
}