using Business.Helper;
using Business.Interfaces;
using Business.Services;
using DAL.Database;
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
            services.AddScoped<EmailService>();
            services.AddScoped<ImagesUrls>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped(typeof(IRepositoryBase<Product>), typeof(RepositoryBase<Product>));
            services.AddScoped(typeof(IRepositoryBase<ProductRating>), typeof(RepositoryBase<ProductRating>));
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGamesService, GamesService>();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddScoped<ValidationFilterAttribute>();

            return services;
        }
    }
}
