using System.Data;
using Dapper;

namespace Blog.DbMigrator;

public static class BlogDbSeeder
{
    public static async Task SeedAsync(IDbConnection connection, CancellationToken cancellationToken = default)
    {
        // only seed if the posts table is empty
        if (await connection.ExecuteScalarAsync<bool>("SELECT EXISTS(SELECT 1 FROM posts);"))
            return;

        await connection.ExecuteAsync(new CommandDefinition(
            """
            insert into posts (slug, title, content_preview, content)
            values
            (
             'first-post', 
             'First Post', 
             'This is the first post preview.', 
             '<p>This is the content of the first post.</p>'
             );
            """, 
            cancellationToken: cancellationToken
        ));
    }
}