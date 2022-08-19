using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpBlog.Repository.Models;

public class Blog
{
    // Id
    public Guid Id { get; set; }
    
    // Title
    public string Title { get; set; }
    
    // Content
    public string Content { get; set; }
    
    // User
    public User User { get; set; }
    
    // User Id
    [ForeignKey(nameof(UserId))]
    public Guid UserId { get; set; }
    
    // Created
    public DateTime Created { get; set; }
    
    // Last Modified
    public DateTime LastModified { get; set; }
    

}