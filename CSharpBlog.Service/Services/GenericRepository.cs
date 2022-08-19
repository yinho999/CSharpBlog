using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpBlog.Exception.Exceptions;
using CSharpBlog.Repository.Context;
using CSharpBlog.Service.Contracts;
using CSharpBlog.Service.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CSharpBlog.Service.Services;

public class GenericRepository<T>: IGenericRepository<T> where T:class
{
    private readonly BloggingContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GenericRepository<T>> _logger;

    public GenericRepository(BloggingContext context, IMapper mapper, ILogger<GenericRepository<T>> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    
    public virtual async Task<T> GetAsync(Guid id)
    {
        // Get item from database
        var item = await _context.Set<T>().FindAsync(id);
        if (item == null)
        {
            // log error 
            _logger.LogError($"{typeof(T).Name} with id: {id} not found");
            throw new NotFoundException(typeof(T).Name, id);
        }
        return item;
    }

    public virtual async Task<TResult> GetAsync<TResult>(Guid id)
    {
        // Get item from database
        var item = await _context.Set<T>().FindAsync(id);
        
        if (item == null)
        {
            // log error 
            _logger.LogError($"{typeof(T).Name} with id: {id} not found");
            throw new NotFoundException(typeof(T).Name, id);
        }
        
        // Map item to DTO
        var result = _mapper.Map<TResult>(item);
        return result;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<IEnumerable<TResult>> GetAllAsync<TResult>()
    {
        var results = await _context.Set<T>().ProjectTo<TResult>(_mapper.ConfigurationProvider).ToListAsync();
        return results;
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TResult> AddAsync<TSource, TResult>(TSource source)
    {
        // transform source to entity
        var entity = _mapper.Map<T>(source);
        // add entity to database
        var result = await AddAsync(entity);
        // Save changes
        await _context.SaveChangesAsync();
        // transform entity to DTO
        var resultDto = _mapper.Map<TResult>(result);
        return resultDto;
    }

    public virtual async Task UpdateAsync(T entity)
    {
         _context.Set<T>().Update(entity);
         await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync<TSource>(Guid id, TSource source) where TSource : IBaseDto
    {
        // Check id is the same as in source 
        if (id != source.Id)
        {
            // throw bad request exception
            _logger.LogError($"Ids do not match {id} {source.Id}");
            throw new BadRequestException("Ids do not match");
        }
        // Get item from database by id
        var entity = await GetAsync(id);
        // Map source to entity
        _mapper.Map(source, entity);
        // Update entity in database
        await UpdateAsync(entity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        // Get item from database by id
        var entity = await GetAsync(id);
        // Remove entity from database
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<bool> Exists(Guid id)
    {
        var entity = await GetAsync(id);
        return entity != null;
    }
}