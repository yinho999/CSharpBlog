namespace CSharpBlog.Service.Dtos.Blog;

public class CreateBlogUserDto: BaseBlogUserDto, IBaseDto
{
    public Guid Id { get;} = Guid.NewGuid();
    // Title
    public string Title { get; set; }
    
    // Content
    public string Content { get; set; }

    // Created
    public DateTime Created { get; } = DateTime.UtcNow;
    
    // Last Modified
    public DateTime LastModified { get; } = DateTime.UtcNow;
}