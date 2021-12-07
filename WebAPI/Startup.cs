using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WebAPI.Infrastructure;

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
            services.AddCloudinary(Configuration);

            services.AddDatabase();

            services.AddJwtToken(Configuration);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddHealthCheck(Configuration.GetConnectionString("DefaultConnection"));

            services.AddMemoryCache();

            services.AddRazorPages();

            services.AddResponseCachingService();

            services.AddServicesCollection();

            services.AddSwaggerDocumentation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");

            Log.Information(env.EnvironmentName);

            app.UseSwaggerDocumentation();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseResponseCompression();

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc");
                endpoints.MapRazorPages();
            });
        }
    }
}