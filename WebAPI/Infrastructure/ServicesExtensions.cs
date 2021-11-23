using Business.Interfaces;
using Business.Services;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace WebAPI.Infrastructure
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServicesCollection(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton(Log.Logger);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGamesService, GamesService>();
            services.AddScoped<EmailService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped(typeof(IRepository<User>), typeof(UserRepository));
            services.AddScoped(typeof(IRepository<Product>), typeof(GamesRepository));
            services.AddRouting(options => options.LowercaseUrls = true);

            return services;
        }
    }
}
