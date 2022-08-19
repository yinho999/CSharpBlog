using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using CSharpBlog.Exception.Exceptions;
using CSharpBlog.Repository.Models;
using CSharpBlog.Service.Contracts;
using CSharpBlog.Service.Dtos.Auth;
using CSharpBlog.Service.Dtos.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CSharpBlog.Service.Services;

public class AuthManager: IAuthManager
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthManager> _logger;
    private User _user;

    private readonly string LoginProvider;
    private readonly string RefreshToken;
    
    public AuthManager(IMapper mapper, UserManager<User> userManager, IConfiguration configuration, ILogger<AuthManager> logger)
    {
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
        LoginProvider = _configuration.GetSection("Authentication:JwtBearer:LoginProvider").Value;
        RefreshToken = _configuration.GetSection("Authentication:JwtBearer:RefreshToken").Value;
    }
    
    public async Task<IEnumerable<IdentityError>> RegisterUser(RegisterUserDto registerUserDto)
    {
        _user = _mapper.Map<User>(registerUserDto);
        // Set up username 
        _user.UserName = _user.Email;
        
        // Auto encrypt password and save user
        var result = await _userManager.CreateAsync(_user, registerUserDto.Password);
        
        // Set up user roles if successful
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(_user, "User");
        }
        
        return result.Errors;
    }

    public async Task<AuthResponseDto?> LoginUser(LoginUserDto loginUserDto)
    {
        _user = await _userManager.FindByEmailAsync(loginUserDto.Email);
        if (_user == null)
        {
            return null;
        }
        var passwordResult = await _userManager.CheckPasswordAsync(_user, loginUserDto.Password);
        if (!passwordResult)
        {
            return null;
        }

        return new AuthResponseDto
        {
            Token = await GenerateToken(),
             Id = _user.Id,
             RefreshToken = await CreateRefreshToken()
        };
    }

    public async Task<AuthResponseDto?> RefreshUserToken(AuthRequestDto authRequestDto)
    {
        var result = await ValidateRefreshToken(authRequestDto);
        if (!result)
        {
            return null;
        }
        await _userManager.UpdateSecurityStampAsync(_user);
        return new AuthResponseDto
        {
            Token = await GenerateToken(),
            Id = _user.Id,
            RefreshToken = await CreateRefreshToken()
        };
    }


    private async Task<string> GenerateToken()
    {
        // Get secret key from appsettings.json
        var secretKey = _configuration.GetSection("Authentication:JwtBearer:SigningCredentials:SigningKey").Value;
        // Encrypt credentials using secret key
        var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256);
        
        // Get Role from user
        var role = await _userManager.GetRolesAsync(_user);
        
        // Role Claims
        var roleClaims = role.Select( x => new Claim(ClaimTypes.Role, x)).ToList();
        
        // User Claims
        var userClaims = await _userManager.GetClaimsAsync(_user);
        
        // Combine all claims to token claims
        var claims = new List<Claim>
        {
            // Username
            new Claim(JwtRegisteredClaimNames.Sub, _user.UserName),
            // Prevent playback attack
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            // User Email
            new Claim(ClaimTypes.Email, _user.Email),
            // User Id
            new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()),
        }.Union(userClaims).Union(roleClaims);

        // Create token with claims
        var token = new JwtSecurityToken(
            issuer: _configuration["Authentication:JwtBearer:Issuer"],
            audience:  _configuration["Authentication:JwtBearer:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["Authentication:JwtBearer:DurationInMinutes"]) ),
            signingCredentials: credentials
        );
        
        // Return token
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<string> CreateRefreshToken()
    {
        // Remove Authentication token from identity table 
        await  _userManager.RemoveAuthenticationTokenAsync(_user, LoginProvider, RefreshToken);
        // Create new Authentication token
        var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, LoginProvider, RefreshToken);
        // Save new Authentication token to identity table
        await _userManager.SetAuthenticationTokenAsync(_user, LoginProvider, RefreshToken, newRefreshToken);
        // Return new Authentication token
        return newRefreshToken;
    }

    private async Task<bool> ValidateRefreshToken(AuthRequestDto authRequestDto)
    {
        try
        {
            // Get jwt token from request
            var jwtToken = authRequestDto.Token;
            // Read token content from jwt token
            var tokenContent = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
         
            // Get email from token
            var email = tokenContent.Claims.First(x => x.Type == ClaimTypes.Email)?.Value;
        
            // return false if user is null
            if (email == null)
            {
                return false;
            }
        
            // Get user from email
            _user = await _userManager.FindByEmailAsync(email);
        
            // return false if user is null
            if (_user == null)
            {
                return false;
            }
        
            // Verify refresh token with refresh token in database
            var result = await _userManager.VerifyUserTokenAsync(_user, LoginProvider, RefreshToken, authRequestDto.RefreshToken);
        
            // return false if refresh token is invalid
            return result;
        }
        catch (System.Exception e)
        {
            _logger.LogError(e.Message);
            throw new BadRequestException("Invalid token");
        }
       

    }
}