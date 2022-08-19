namespace CSharpBlog.Service.Dtos.Auth;

public class AuthRequestDto
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}