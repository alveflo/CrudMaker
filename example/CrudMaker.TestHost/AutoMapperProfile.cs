using AutoMapper;
using CrudMaker.TestHost.Database;
using CrudMaker.TestHost.Dtos;

namespace CrudMaker.TestHost
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BlogDto, Blog>()
                .ReverseMap();

            CreateMap<PostDto, Post>()
                .ReverseMap();
        }
    }
}
