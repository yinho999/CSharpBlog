namespace CSharpBlog.Service.Dtos.Blog;

public class GetBlogDto: BaseBlogDto, IBaseDto
{
    // Id
    public Guid Id { get; set; }
    
    // Title
    public string Title { get; set; }
    
    // Content
    public string Content { get; set; }
        
    // User Id
    public Guid UserId { get; set; }
    
    // Created
    public DateTime Created { get; set; }
    
    // Last Modified
    public DateTime LastModified { get; set; }
}