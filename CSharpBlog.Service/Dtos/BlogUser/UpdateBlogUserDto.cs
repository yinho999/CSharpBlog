namespace CSharpBlog.Service.Dtos.Blog;

public class UpdateBlogUserDto: BaseBlogUserDto, IBaseDto
{
    // Id
    public Guid Id { get; set; }
    
    // Title
    public string Title { get; set; }
    
    // Content
    public string Content { get; set; }
    
    // Last Modified
    public DateTime LastModified { get; } = DateTime.UtcNow;
}