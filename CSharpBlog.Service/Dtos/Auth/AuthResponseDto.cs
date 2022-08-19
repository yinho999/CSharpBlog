namespace CSharpBlog.Service.Dtos.Auth;

public class AuthResponseDto
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}