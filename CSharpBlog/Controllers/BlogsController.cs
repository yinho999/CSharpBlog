using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CSharpBlog.Repository.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSharpBlog.Repository.Models;
using CSharpBlog.Service.Contracts;
using CSharpBlog.Service.Dtos.Blog;
using Microsoft.AspNetCore.Authorization;

namespace CSharpBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BloggingContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BlogsController> _logger;
        private readonly IBlogsRepository _blogsRepository;

        public BlogsController(BloggingContext context, IMapper mapper, ILogger<BlogsController> logger, IBlogsRepository blogsRepository)
     
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _blogsRepository = blogsRepository;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBlogDto>>> GetAllBlogs()
        {
            var blogs = await _blogsRepository.GetAllAsync<GetBlogDto>();
            return Ok(blogs);
        }

        // GET: api/Blogs/5
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetBlogDto>> GetBlog(Guid id)
        {
            // string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // _logger.LogInformation($"This is the user id {userId}");
            var blog = await _blogsRepository.GetAsync<GetBlogDto>(id);
            return blog;
        }
        
        
        // Restricted to admin
        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize( Roles = "Admin")]
        public async Task<IActionResult> PutBlog(Guid id, UpdateBlogDto updateBlogDto)
        {
            
            try
            {
                await _blogsRepository.UpdateAsync(id, updateBlogDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BlogExists(id))
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize( Roles = "Admin")]
        public async Task<ActionResult<Blog>> PostBlog(CreateBlogDto blog)
        {
            await _blogsRepository.AddAsync<CreateBlogDto,GetBlogDto>(blog);
            return CreatedAtAction(nameof(GetBlog), new { id = blog.Id }, blog);
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        [Authorize( Roles = "Admin")]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            await _blogsRepository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> BlogExists(Guid id)
        {
             return await  _blogsRepository.Exists(id);
        }
    }
}
