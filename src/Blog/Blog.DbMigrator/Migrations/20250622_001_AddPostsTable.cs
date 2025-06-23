using FluentMigrator;

namespace Blog.DbMigrator.Migrations;

[Migration(20250622_001)]
public sealed class AddBlogsTable : Migration 
{
    public override void Up()
    {
        Execute.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");
        
        Create.Table("posts")
            .WithColumn("id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("title").AsString(200).NotNullable()
            .WithColumn("slug").AsString(200).NotNullable().Unique()
            .WithColumn("content").AsString().NotNullable()
            .WithColumn("content_preview").AsString(1000).NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable().WithDefault(SystemMethods.CurrentDateTimeOffset);
        
        Create.Index("idx_posts_slug")
            .OnTable("posts")
            .OnColumn("slug").Ascending()
            .WithOptions().Unique();
        
        Create.Index("idx_posts_created_at")
            .OnTable("posts")
            .OnColumn("created_at").Ascending();
    }

    public override void Down()
    {
        Delete.Table("posts");
    }
}