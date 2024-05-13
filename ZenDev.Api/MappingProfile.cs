using AutoMapper;
using ZenDev.Api.ApiModels;
using ZenDev.BusinessLogic.Models;
using ZenDev.Persistence.Entities;

namespace ZenDev.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExampleEntity, ExampleApiModel>().ReverseMap();
            CreateMap<ResultModel, ResultApiModel>().ReverseMap();
            CreateMap<UserEntity, UserApiModel>().ReverseMap();
        }
    }
}