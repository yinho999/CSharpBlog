using AutoMapper;
using CSharpBlog.Repository.Models;
using CSharpBlog.Service.Dtos.Blog;
using CSharpBlog.Service.Dtos.User;

namespace CSharpBlog.Service.Configuration;

public class MapperConfig:Profile
{
    public MapperConfig()
    {
        // Blog
        CreateMap<BaseBlogDto, Blog>().ReverseMap();
        CreateMap<GetBlogDto, Blog>().ReverseMap();
        CreateMap<UpdateBlogDto, Blog>().ReverseMap();
        CreateMap<CreateBlogDto, Blog>().ReverseMap();
        
        // Blog User
        CreateMap<CreateBlogUserDto, Blog>().ReverseMap();
        CreateMap<UpdateBlogUserDto, Blog>().ReverseMap();
        
        // User
        CreateMap<User, LoginUserDto>().ReverseMap();
        CreateMap<RegisterUserDto,User>()
            .ReverseMap();
    }
}