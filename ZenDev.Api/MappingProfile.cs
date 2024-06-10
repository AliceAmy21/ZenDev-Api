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
            CreateMap<UserResultApiModel, UserResultModel>().ReverseMap();
            CreateMap<PersonalGoalEntity, PersonalGoalApiModel>()
                .ForMember(dest => dest.Exercise, opt => opt.MapFrom(src => src.ExerciseEntity))
                .ReverseMap();
            CreateMap<ExerciseEntity, ExerciseApiModel>().ReverseMap();
            CreateMap<ExerciseTypeEntity, ExerciseTypeApiModel>().ReverseMap();
            CreateMap<GroupEntity, GroupApiModel>()
                .ForMember(dest => dest.ExerciseType, opt => opt.MapFrom(src => src.ExerciseTypeEntity))
                .ReverseMap();
            CreateMap<GroupResultApiModel, GroupResultModel>().ReverseMap();
            CreateMap<GroupInvitationEntity, GroupInvitationApiModel>().ReverseMap();
        }
    }
}