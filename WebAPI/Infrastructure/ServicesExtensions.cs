using Business.Helper;
using Business.Interfaces;
using Business.Services;
using DAL.Database;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WebAPI.ActionFilters;

namespace WebAPI.Infrastructure
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServicesCollection(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton(Log.Logger);
            services.AddScoped<EmailService>();
            services.AddScoped<ImagesUrls>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped(typeof(ICachingData<User>), typeof(CachingData<User>));

            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGamesService, GamesService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddScoped<LogActionFilterAttribute>();

            return services;
        }
    }
}