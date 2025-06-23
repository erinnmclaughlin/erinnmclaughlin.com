using FluentMigrator;

namespace Blog.DbMigrator.Migrations;

[Migration(20250622_001)]
public sealed class AddBlogsTable : Migration 
{
    public override void Up()
    {
        Create.Table("posts")
            .WithColumn("id").AsGuid().PrimaryKey().Identity()
            .WithColumn("title").AsString(256).NotNullable()
            .WithColumn("slug").AsString(256).NotNullable().Unique()
            .WithColumn("content").AsString().NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);
        
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