using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace JWTMaster.Service.Library
{
    public class JwtTokenMaker
    {
        private IConfiguration _configuration;
        public JwtTokenMaker(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string GenerateToken()
        {

            var jwtConfig = _configuration.GetSection("JetonConfig").Get<JwtConfig>();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.MyKey));


            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                    issuer: jwtConfig.Issuer,
                    audience: jwtConfig.Audience,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddSeconds(30),
                    signingCredentials: credentials

                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
    }
}
