using AutoMapper;
using CSharpBlog.Exception.Exceptions;
using CSharpBlog.Repository.Context;
using CSharpBlog.Repository.Models;
using CSharpBlog.Service.Contracts;
using CSharpBlog.Service.Dtos;
using Microsoft.Extensions.Logging;

namespace CSharpBlog.Service.Services;

public class BlogsRepository: GenericRepository<Blog>, IBlogsRepository
{
    private readonly BloggingContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<BlogsRepository> _logger;

    public BlogsRepository(BloggingContext context, IMapper mapper, ILogger<BlogsRepository> logger) : base(context,
        mapper, logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    
}