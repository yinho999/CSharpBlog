namespace CSharpBlog.Service.Dtos.Blog;

public class CreateBlogDto: BaseBlogDto, IBaseDto
{
    public Guid Id { get;} = Guid.NewGuid();
    // Title
    public string Title { get; set; }
    
    // Content
    public string Content { get; set; }
    
    // User Id
    public Guid UserId { get; set; }
    
    // Created
    public DateTime Created { get; } = DateTime.UtcNow;
    
    // Last Modified
    public DateTime LastModified { get; } = DateTime.UtcNow;
}