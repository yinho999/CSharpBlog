using CSharpBlog.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSharpBlog.Repository.Context.Configuration;

public class BlogConfiguration: IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        // Seeding data
        builder.HasData(
            new Blog
            {
                Id = new Guid("fd6c9dc8-97af-4ad5-a361-a79e9b2b60da"),
                Title = "C# Blog",
                Content = "C# Blog",
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                UserId = new Guid("354fa244-3c83-438b-9f2b-284707fd3936")
            },
            new Blog
            {
                Id = new Guid("fd6c9dc8-97af-4ad5-a361-a79e9b2b60db"),
                Title = "C# Blog 2",
                Content = "C# Blog 2",
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                UserId = new Guid("354fa244-3c83-438b-9f2b-284707fd3935")
            }
        );
    }
}