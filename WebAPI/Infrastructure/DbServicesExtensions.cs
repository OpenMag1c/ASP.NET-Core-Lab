using System.IO;
using DAL.Database;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace WebAPI.Infrastructure
{
    public static class DbServicesExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            Log.Information(connectionString);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<User, IdentityRole<int>>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}