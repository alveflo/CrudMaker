using AutoMapper;
using Crudify.TestHost.Database;
using Crudify.TestHost.Dtos;

namespace Crudify.TestHost
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BlogDto, Blog>();
            CreateMap<PostDto, Post>();
        }
    }
}
