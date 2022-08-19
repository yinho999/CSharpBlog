using CSharpBlog.Service.Dtos.Auth;
using CSharpBlog.Service.Dtos.User;
using Microsoft.AspNetCore.Identity;

namespace CSharpBlog.Service.Contracts;

public interface IAuthManager
{
    Task<IEnumerable<IdentityError>> RegisterUser( RegisterUserDto registerUserDto );
    Task<AuthResponseDto?> LoginUser(LoginUserDto loginUserDto);
    Task<AuthResponseDto?> RefreshUserToken (AuthRequestDto authRequestDto );

}