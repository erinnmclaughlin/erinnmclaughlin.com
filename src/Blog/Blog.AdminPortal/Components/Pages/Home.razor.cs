using Dapper;
using Npgsql;

namespace Blog.AdminPortal.Components.Pages;

public sealed partial class Home
{
    private readonly NpgsqlDataSource _dataSource;

    public Home(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    private IReadOnlyList<BlogPost>? Posts { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await using var connection = await _dataSource.OpenConnectionAsync();
        var posts =
            await connection.QueryAsync<BlogPost>(
                """
                select id, title, created_at as "CreatedAt"
                from posts
                order by created_at desc
                limit 10
                """
            );

        Posts = posts.ToList();
    }

    private sealed record BlogPost(Guid Id, string Title, DateTime CreatedAt);
}