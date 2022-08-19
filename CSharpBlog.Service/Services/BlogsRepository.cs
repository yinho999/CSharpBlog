using AutoMapper;
using CSharpBlog.Exception.Exceptions;
using CSharpBlog.Repository.Context;
using CSharpBlog.Repository.Models;
using CSharpBlog.Service.Contracts;
using CSharpBlog.Service.Dtos;
using CSharpBlog.Service.Dtos.Blog;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CSharpBlog.Service.Services;

public class BlogsRepository: GenericRepository<Blog>, IBlogsRepository
{
    private readonly BloggingContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<BlogsRepository> _logger;
    private readonly UserManager<User> _userManager;

    public BlogsRepository(BloggingContext context, IMapper mapper, ILogger<BlogsRepository> logger, UserManager<User> userManager) : base(context,
        mapper, logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
        _userManager = userManager;
    }
    
    // Get blogs by user id and blog id
    public async Task<TResult> GetBlogByIdByUser<TResult>(Guid userId, Guid blogId)
    {
        var blog = await GetAsync(blogId);
        if (blog == null)
        {
            _logger.LogError($"Blog with id: {blogId} not found");
            throw new NotFoundException(typeof(Blog).Name, blogId);
        }
        if (blog.UserId != userId)
        {
            _logger.LogError($"Blog with id: {blogId} not found in this user {userId}");
            throw new NotFoundException(typeof(Blog).Name, blogId);
        }
        _logger.LogInformation($"Blog with id: {blog.Id} found in this user {userId}");
        return _mapper.Map<TResult>(blog);
    }
    
    
    // Get all blogs for a specific user (userId)
    public async Task<IEnumerable<TResult>> GetAllAsyncByUser<TResult>(Guid userId)
    {
        var blogs = await GetAllAsync();
        var blogsForUser = blogs.Where(b => b.UserId == userId);
        var blogsForUserDtos = _mapper.Map<IEnumerable<TResult>>(blogsForUser);
        return blogsForUserDtos;
    }
    
    // Update a new blog for a specific user (userId)
    public async Task UpdateAsyncByUser(Guid userId, Guid blogId, UpdateBlogUserDto updateBlogUserDto)
    {
        var blogExist = await BlogExistsByUser(userId, blogId);
        if (!blogExist)
        {
            _logger.LogError($"Blog with id: {blogId} not found in this user {userId}");
            throw new NotFoundException(typeof(Blog).Name, blogId);
        }
        // Update Blog
        await UpdateAsync(blogId, updateBlogUserDto);
    }

    public async Task DeleteAsyncByUser(Guid userId, Guid blogId)
    {
        
        var blogExist = await BlogExistsByUser(userId, blogId);
        if (!blogExist)
        {
            _logger.LogError($"Blog with id: {blogId} not found in this user {userId}");
            throw new NotFoundException(typeof(Blog).Name, blogId);
        }
        // Delete Blog
        await DeleteAsync(blogId);
    }

    // Create a new blog for a specific user (userId)
    public async Task<TResult> AddAsyncByUser<TSource, TResult>(TSource source, User user)
    {
        var blog = _mapper.Map<Blog>(source);
        blog.UserId = user.Id;
        await AddAsync(blog);
        return _mapper.Map<TResult>(blog);
    }
    
    private async Task<bool> BlogExistsByUser(Guid userId, Guid blogId)
    {
        var blog = await GetAsync(blogId);
        return blog.UserId == userId;
    }
}