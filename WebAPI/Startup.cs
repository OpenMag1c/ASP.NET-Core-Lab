using System;
using System.IO;
using System.Text;
using Business.Authentication;
using Business.Authentication.Login;
using Business.Interfaces;
using Business.Services;
using DAL.Database;
using DAL.Models;
using MediatR;
using DAL.Repositories;
using DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using WebAPI.AutoMapper;
using WebAPI.HealthCheck;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            // устанавливаем контекст данных
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddMediatR(typeof(LoginHandler).Assembly);

            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                option.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);

            var idBuilder = services.AddIdentityCore<User>();
            var identityBuilder = new IdentityBuilder(idBuilder.UserType, idBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<ApplicationDbContext>();
            identityBuilder.AddSignInManager<SignInManager<User>>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                    };
                });
            services.AddScoped<IJwtGenerator, JwtGenerator>();

            //services.AddDefaultIdentity<User>(options =>
            //    {
            //        options.SignIn.RequireConfirmedAccount = true;
            //        options.User.RequireUniqueEmail = true;
            //    })
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //HealthCheck
            services.AddHealthChecks()
                // Add a health check for a SQL Server database
                .AddCheck(
                    "usersdb-check",
                    new SqlConnectionHealthCheck(connectionString),
                    HealthStatus.Unhealthy,
                    new string[] { "usersdb" });

            services.AddControllers();
            services.AddSingleton(Log.Logger);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped(typeof(IRepository<User>), typeof(UserRepository));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSerilogRequestLogging();

            app.UseMvcWithDefaultRoute();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    endpoints.MapHealthChecks("/hc");
            //});
        }
    }
}