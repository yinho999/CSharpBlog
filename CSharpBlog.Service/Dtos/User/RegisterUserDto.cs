using System.ComponentModel.DataAnnotations;

namespace CSharpBlog.Service.Dtos.User;

public class RegisterUserDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StringLength(15, ErrorMessage = "Your Password is limited to {2} to {1} characters", MinimumLength = 6)]
    public string Password { get; set; }
}