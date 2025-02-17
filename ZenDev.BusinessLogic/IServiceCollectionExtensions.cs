﻿using Microsoft.Extensions.DependencyInjection;
using ZenDev.BusinessLogic.Services;
using ZenDev.BusinessLogic.Services.Interfaces;

namespace ZenDev.BusinessLogic
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterBusinessLayerDependencies(this IServiceCollection services)
        {
            services.AddScoped<IExampleService, ExampleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonalGoalService, PersonalGoalService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IGroupInvitationService, GroupInvitationService>();
            services.AddScoped<IChallengeService, ChallengeService>();
            services.AddTransient<IPointsService, PointsService>();
            services.AddScoped<ILeaderBoardService, LeaderBoardService>();
            services.AddScoped<IMindfulnessService, MindfulnessService>();
            services.AddScoped<IAchievementService, AchievementService>();
            services.AddScoped<ITournamentService,TournamentService>();
            services.AddScoped<IMessageService,MessageService>();
        }
    }
}
