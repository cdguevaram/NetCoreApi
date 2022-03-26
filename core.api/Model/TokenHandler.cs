using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace core.Api.Model
{
    public class TokenHandler
    {
        public static object GenerateJwt(LoginModel userInfo, string secKey, string Issuer, ICollection<string> Roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> lClaim = new List<Claim>();
            lClaim.Add(new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName));
            lClaim.Add(new Claim(JwtRegisteredClaimNames.Email, userInfo.UserName));
            lClaim.Add(new Claim("DateOfJoing", DateTime.Now.ToString("yyyy-MM-dd")));
            lClaim.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            lClaim.Add(new Claim(ClaimTypes.Name, userInfo.UserName ));
            lClaim.Add(new Claim(ClaimTypes.Email, userInfo.UserName));
           

            foreach (var rol in Roles)
            {
                lClaim.Add(new Claim(ClaimTypes.Role, rol));
            }



            var token = new JwtSecurityToken(Issuer,
              Issuer,
              lClaim.ToArray(),
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var finaltoken = new JwtSecurityTokenHandler().WriteToken(token);

            var rt = new
            {
                Token = "Bearer " + finaltoken,
                Type = "Bearer"
            };
            return rt;
        }
    }
}
