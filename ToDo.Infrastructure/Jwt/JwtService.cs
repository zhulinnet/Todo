using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDo.Infrastructure.Identity;

namespace ToDo.Infrastructure.Jwt
{
    public class JwtService : IJwtService
    {
        public IConfiguration Configuration { get; }

        public JwtService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string CreateToken(string userId, string userName)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:securityKey"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new Claim[]
            {
                new Claim(CustomClaimTypes.UserId, userId),
                new Claim(CustomClaimTypes.Account, userName),
                new Claim(JwtRegisteredClaimNames.Iss, Configuration["JWT:issuer"]),
                new Claim(JwtRegisteredClaimNames.Aud,  Configuration["JWT:audience"]),
                new Claim(JwtRegisteredClaimNames.Exp,  Configuration["JWT:expires"])
            };
            SecurityToken token = new JwtSecurityToken(
                issuer: Configuration["JWT:issuer"],
                audience: Configuration["JWT:audience"],
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddMinutes(Configuration.GetValue<int>("JWT:expires")),
                claims: claims
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
