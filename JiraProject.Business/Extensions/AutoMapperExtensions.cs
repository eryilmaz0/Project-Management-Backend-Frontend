using AutoMapper;
using JiraProject.Business.MapperProfile.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace JiraProject.Entities.Extensions
{
    public static class AutoMapperExtensions
    {
        
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            return services.AddAutoMapper(typeof(AutoMapperProfile));
        }

    }
}