using System.Net;
using CSharpBlog.Service.Contracts;
using CSharpBlog.Service.Dtos.Auth;
using CSharpBlog.Service.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpBlog.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthManager _authManager;

    public AccountController(IAuthManager authManager)
    {
        _authManager = authManager;
    }
    

    // Register
    [HttpPost("register")]
    // Potentially return 201
    [ProducesResponseType(StatusCodes.Status201Created)]
    // Potentially return 409
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    // Potentially return 500
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        var errors =  await _authManager.RegisterUser(registerUserDto);
        if (errors.Any())
        {
            return Conflict(errors);
        }
        return Created("User created", null);
    }
    
    // Login
    [HttpPost("login")]
    // Potentially return 200
    [ProducesResponseType(StatusCodes.Status200OK)]
    // Potentially return 401
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    // Potentially return 500
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        var authResult = await _authManager.LoginUser(loginUserDto);
        if (authResult is null)
        {
            return Unauthorized();
        }
        return Ok(authResult);
    }
    
    /// <summary>
    /// This is a test method
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("refreshtoken")]
    // Potentially return 401
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    // Potentially return 500
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // Potentially return 200
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> RefreshToken([FromBody] AuthRequestDto request)
    {
        var authResponse = await _authManager.RefreshUserToken(request);
        if (authResponse == null)
        {
            // 401
            return Unauthorized();
        }

        return Ok(authResponse);
    }
    
}