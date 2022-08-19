using CSharpBlog.Repository.Models;
using CSharpBlog.Service.Dtos.Blog;

namespace CSharpBlog.Service.Contracts;

public interface IBlogsRepository: IGenericRepository<Blog>
{
    Task<TResult> GetBlogByIdByUser<TResult>(Guid userId, Guid blogId);
    Task<IEnumerable<TResult>> GetAllAsyncByUser<TResult>(Guid userId);

    Task<TResult> AddAsyncByUser<TSource, TResult>(TSource source, User user);
    
    Task UpdateAsyncByUser(Guid userId, Guid blogId, UpdateBlogUserDto updateBlogUserDto);
    Task DeleteAsyncByUser(Guid userId, Guid blogId);
}