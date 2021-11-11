using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business.JWT
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public JwtGenerator(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }

            var jwtAppSettingOptions = _configuration.GetSection("JwtIssuerOptions");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAppSettingOptions["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(jwtAppSettingOptions["JwtExpireMinutes"]));
            var token = new JwtSecurityToken(
                jwtAppSettingOptions["JwtIssuer"],
                jwtAppSettingOptions["JwtAudience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
