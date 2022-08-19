using CSharpBlog.Repository.Context.Configuration;
using CSharpBlog.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CSharpBlog.Repository.Context;

public class BloggingContext: IdentityDbContext<User, UserRole,Guid>
{
     // Initialize the database 
     public BloggingContext(DbContextOptions<BloggingContext> options)
         : base(options)
     {
         
     }
     
     // Link tables with the database
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
     
     // Initialize the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Initialize the tables
            modelBuilder.Entity<Blog>().ToTable("Blog");
            modelBuilder.Entity<User>().ToTable("User");
            
            // Initialize the primary key
            modelBuilder.Entity<Blog>().HasKey(b => b.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            
            // Initialize the foreign key
            modelBuilder.Entity<Blog>().HasOne(b => b.User).WithMany(u => u.Blogs).HasForeignKey(b => b.UserId);
            
            // Seeding Data
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new BlogConfiguration());
        }
    
}