using System.Security.Claims;
using AutoMapper;
using CSharpBlog.Repository.Context;
using CSharpBlog.Repository.Models;
using CSharpBlog.Service.Contracts;
using CSharpBlog.Service.Dtos.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSharpBlog.Controllers;

[Route("api/Blogs/User/")]
[ApiController]
public class BlogsUserController : ControllerBase
{
    private readonly BloggingContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<BlogsController> _logger;
    private readonly IBlogsRepository _blogsRepository;

    public BlogsUserController(BloggingContext context, IMapper mapper, ILogger<BlogsController> logger, IBlogsRepository blogsRepository)

    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
        _blogsRepository = blogsRepository;
    }
    
    // GET: api/Blogs/5
    [HttpGet("{userId:guid}/{blogId:guid}")]
    public async Task<ActionResult<GetBlogDto>> GetBlog(Guid userId, Guid blogId)
    {
        var blog = await _blogsRepository.GetBlogByIdByUser<GetBlogDto>(userId, blogId);
        if (blog == null)
        {
            return NotFound();
        }
        return Ok(blog);
    }
  
    
    // Get all blogs for a user
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<IEnumerable<GetBlogDto>>> GetBlogByUserId(Guid userId)
    {
        var blogs = await _blogsRepository.GetAllAsyncByUser<GetBlogDto>(userId);
        return Ok(blogs);
    }
    
    
    
    // PUT: api/Blogs/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{blogId:guid}")]
    [Authorize]
    public async Task<IActionResult> PutBlogByUserId(Guid blogId, UpdateBlogUserDto updateBlogDto)
    {
        var user = await GetUser();
        try
        {
            await _blogsRepository.UpdateAsyncByUser(user.Id ,blogId, updateBlogDto);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await BlogExists(blogId))
            {
                return NotFound();
            }
        }

        return NoContent();
    }
    
    // POST: api/Blogs
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Blog>> PostBlogByUserId(CreateBlogUserDto blog)
    {
        // Get current user
        var user = await GetUser();
        _logger.LogInformation("The current user is {0}", user.Id);
        var createdBlog = await _blogsRepository.AddAsyncByUser<CreateBlogUserDto,GetBlogDto>(blog, user);
        return CreatedAtAction(nameof(GetBlog), new { userId = createdBlog.UserId, blogId = createdBlog.Id }, createdBlog);
    }

    
    // DELETE: api/Blogs/5
    [HttpDelete("{blogId:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteBlogByUserId(Guid blogId)
    {
        var user = await GetUser();
        await _blogsRepository.DeleteAsyncByUser(user.Id, blogId);
        return NoContent();
    }
    
    private async Task<bool> BlogExists(Guid id)
    {
        return await  _blogsRepository.Exists(id);
    }
    
    private async Task<User> GetUser()
    {
        var userEmail =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
         _logger.LogInformation($"user is {user.Id}");
        return user;
    }
    
  
}