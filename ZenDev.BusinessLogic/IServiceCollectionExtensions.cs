using Microsoft.Extensions.DependencyInjection;
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
        }
    }
}
