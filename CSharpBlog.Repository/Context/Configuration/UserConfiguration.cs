using CSharpBlog.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSharpBlog.Repository.Context.Configuration;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Seeding User data
        builder.HasData(
            new User
            {
                Id = new Guid("354fa244-3c83-438b-9f2b-284707fd3935"),
                UserName = "user",
                Email = "asd@gmail.com",
                // Password = "123456",
                Name = "User",
            },
            new User
            {
                Id = new Guid("354fa244-3c83-438b-9f2b-284707fd3936"),
                UserName = "user1",
                Email = "asdf@gmail.com",
                // Password = "123456",
                Name = "User1",
            }
        );
    }
}