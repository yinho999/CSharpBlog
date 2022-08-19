using Microsoft.AspNetCore.Identity;

namespace CSharpBlog.Repository.Models;

public class User: IdentityUser<Guid>
{

     // Name
     public string Name { get; set; }

     // Blogs 
     public virtual ICollection<Blog>? Blogs { get; set; }
     
     
}