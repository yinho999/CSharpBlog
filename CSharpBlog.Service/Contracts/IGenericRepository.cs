using CSharpBlog.Service.Dtos;

namespace CSharpBlog.Service.Contracts;

public interface IGenericRepository<T> where T:class 
{
    // Get 
    Task<T> GetAsync (Guid id);
    Task<TResult> GetAsync<TResult>(Guid id);
    // Get All
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<TResult>> GetAllAsync<TResult>();
    // Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters);
    
    // Add 
    Task<T> AddAsync(T entity);
    Task<TResult> AddAsync<TSource, TResult>(TSource source);
    
    // Update
    Task UpdateAsync(T entity);
    Task UpdateAsync<TSource>(Guid id, TSource source) where TSource : IBaseDto;
    
    // Delete
    Task DeleteAsync(Guid id);
    Task<bool> Exists(Guid id);
}