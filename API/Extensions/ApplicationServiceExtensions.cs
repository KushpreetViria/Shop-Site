using API.Data;
using API.Data.Repository;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        //extension method of IserviceCollection
        //add anything you want injectable to this scope
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config){
            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IItemRepository,ItemRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}