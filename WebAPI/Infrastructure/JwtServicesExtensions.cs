﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Business.Interfaces;
using Business.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Infrastructure
{
    public static class JwtServicesExtensions
    {
        public static IServiceCollection AddJwtToken(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            var jwtAppSettingOptions = configuration.GetSection("JwtIssuerOptions");
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtAppSettingOptions["JwtIssuer"],
                        ValidAudience = jwtAppSettingOptions["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAppSettingOptions["JwtKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddScoped<IJwtGenerator, JwtGenerator>();

            return services;
        }

        public static IApplicationBuilder UseJwtToken(this IApplicationBuilder app)
        {

            return app;
        }
    }
}
