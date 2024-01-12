using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class JWTService
    {
        private string SecurityKey = "Secure Key Secure Key Secure Key Secure Key";
        public string Generate(int id)
        {
            var SymmetricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));  
            var credentials = new SigningCredentials(SymmetricSecurity, SecurityAlgorithms.HmacSha256Signature);
            var header= new JwtHeader(credentials);
            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));
            var securityToken = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecurityKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters {ValidateIssuer=false, ValidateIssuerSigningKey=true
            ,IssuerSigningKey=new SymmetricSecurityKey(key), ValidateAudience=false }, out SecurityToken ValidatedToken);
            return (JwtSecurityToken) ValidatedToken;
        }
    }
}
