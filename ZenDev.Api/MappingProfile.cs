using AutoMapper;
using ZenDev.Api.ApiModels;
using ZenDev.Api.ApiModels.Strava;
using ZenDev.BusinessLogic.Models;
using ZenDev.Common.Models;
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
            CreateMap<UserGroupBridgeEntity, UserGroupBridgeApiModel>().ReverseMap(); 
            CreateMap<ChallengeEntity, ChallengeApiModel>()
            .ForMember(dest=>dest.ExerciseApiModel, opt=>opt.MapFrom(src=>src.ExerciseEntity))
            .ForMember(dest=>dest.GroupApiModel, opt=>opt.MapFrom(src=>src.GroupEntity))
            .ReverseMap();
            CreateMap<UserChallengeBridgeEntity, UserChallengeBridgeApiModel>().ReverseMap();
            CreateMap<ChallengeListModel, ChallengeListApiModel>()
            .ForMember(dest=>dest.ExerciseApiModel, opt=>opt.MapFrom(src=>src.ExerciseEntity))
            .ForMember(dest=>dest.GroupApiModel, opt=>opt.MapFrom(src=>src.GroupEntity))
            .ReverseMap();
            CreateMap<ChallengeCreationModel, ChallengeCreationApiModel>().ReverseMap();
            CreateMap<ChallengeUpdateModel, ChallengeUpdateApiModel>().ReverseMap();
            CreateMap<GroupEntity, GroupListApiModel>()
                .ForMember(dest => dest.ExerciseType, opt => opt.MapFrom(src => src.ExerciseTypeEntity))
                .ReverseMap();
            CreateMap<GroupResultModel, GroupResultApiModel>().ReverseMap();
            CreateMap<GroupInvitationEntity, GroupInvitationApiModel>().ReverseMap();
            CreateMap<UserInviteModel, UserInviteApiModel>().ReverseMap();
            CreateMap<NotificationModel, NotificationApiModel>().ReverseMap();
            CreateMap<AchievementEntity, AchievementApiModel>().ReverseMap();
             CreateMap<UserAchievementBridgeEntity, UserAchievementBridgeApiModel>().ReverseMap();
            CreateMap<ActivitySummaryResponse, ActivitySummaryApiModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Athlete, opt => opt.MapFrom(src => src.athlete))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Distance, opt => opt.MapFrom(src => src.distance))
                .ForMember(dest => dest.MovingTime, opt => opt.MapFrom(src => src.moving_time))
                .ForMember(dest => dest.ElapsedTime, opt => opt.MapFrom(src => src.elapsed_time))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.type))
                .ForMember(dest => dest.SportType, opt => opt.MapFrom(src => src.sport_type))
                .ForMember(dest => dest.WorkoutType, opt => opt.MapFrom(src => src.workout_type))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.start_date))
                .ForMember(dest => dest.StartDateLocal, opt => opt.MapFrom(src => src.start_date_local))
                .ForMember(dest => dest.Timezone, opt => opt.MapFrom(src => src.timezone))
                .ForMember(dest => dest.UtcOffset, opt => opt.MapFrom(src => src.utc_offset)) // Convert double to int
                .ForMember(dest => dest.StartLatlng, opt => opt.MapFrom(src => src.start_latlng))
                .ForMember(dest => dest.EndLatlng, opt => opt.MapFrom(src => src.end_latlng))
                .ForMember(dest => dest.LocationCity, opt => opt.MapFrom(src => src.location_city))
                .ForMember(dest => dest.LocationState, opt => opt.MapFrom(src => src.location_state))
                .ForMember(dest => dest.LocationCountry, opt => opt.MapFrom(src => src.location_country))
                .ForMember(dest => dest.Map, opt => opt.MapFrom(src => src.map))
                .ForMember(dest => dest.AverageSpeed, opt => opt.MapFrom(src => src.average_speed))
                .ForMember(dest => dest.MaxSpeed, opt => opt.MapFrom(src => src.max_speed))
                .ForMember(dest => dest.Kilojoules, opt => opt.MapFrom(src => src.kilojoules))
                .ForMember(dest => dest.HasHeartrate, opt => opt.MapFrom(src => src.has_heartrate))
                .ForMember(dest => dest.AverageHeartrate, opt => opt.MapFrom(src => src.average_heartrate))
                .ForMember(dest => dest.MaxHeartrate, opt => opt.MapFrom(src => src.max_heartrate));

            CreateMap<AthleteResponse, AthleteApiModel>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
              .ForMember(dest => dest.ResourceState, opt => opt.MapFrom(src => src.resource_state));

            CreateMap<MapResponse, MapApiModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.SummaryPolyline, opt => opt.MapFrom(src => src.summary_polyline));

            CreateMap<ActivitySummaryApiModel, ActivityPointsApiModel>().ReverseMap();

        }
    }
}