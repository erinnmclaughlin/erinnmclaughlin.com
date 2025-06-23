using Dapper;
using Microsoft.AspNetCore.Components;
using Npgsql;

namespace Blog.AdminPortal.Components.Pages;

public sealed partial class Create
{
    private readonly NpgsqlDataSource _dataSource;
    private readonly ILogger<Create> _logger;
    private readonly NavigationManager _navigationManager;

    [SupplyParameterFromForm] public FormModel Form { get; set; } = new();
    
    public Create(NpgsqlDataSource dataSource, NavigationManager navigationManager, ILogger<Create> logger)
    {
        _dataSource = dataSource;
        _navigationManager = navigationManager;
        _logger = logger;
    }

    private async Task CreatePost()
    {
        _logger.LogInformation("Creating post with title: {Title}", Form.Title);
        await using var connection = await _dataSource.OpenConnectionAsync();
        
        await connection.ExecuteAsync(
            """
            insert into posts (id, title, slug, content, content_preview)
            values (@Id, @Title, @Slug, @Content, @ContentPreview)
            """,
            new
            {
                Id = Guid.CreateVersion7(),
                Form.Title,
                Slug = Form.Title.ToLower().Replace(' ', '-').Replace('.', '-'),
                Form.Content,
                Form.ContentPreview
            }
        );
        
        _navigationManager.NavigateTo("/");
    }

    public sealed class FormModel
    {
        public string Title { get; set; } = string.Empty;
        public string ContentPreview { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}