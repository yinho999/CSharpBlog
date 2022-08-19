using CSharpBlog.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSharpBlog.Repository.Context.Configuration;

public class RoleConfiguration:IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasData(
            new UserRole
            {
                Id = new Guid("4320c03b-abe9-4424-a278-9cbb1b6f4224"),
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new UserRole
            {
                Id = new Guid("ed82663d-f917-4a6e-8f42-c51dbab9af72"),
                Name = "User",
                NormalizedName = "USER"
            }
        );
    }
}