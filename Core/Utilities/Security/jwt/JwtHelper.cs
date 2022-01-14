using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.jwt
{
    public class JwtHelper : ITokenHelper
    {
        private IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions { get; }
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration _configuration)
        {
            Configuration= _configuration;
            _tokenOptions=Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _accessTokenExpiration=DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            var securityKey=SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentinalHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token=jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token=token,
                Expiration=_accessTokenExpiration
            };

         }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,User user,SigningCredentials signingCredentials,List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now, //token ın expiresin bilgisi şuandan önce ise geçerli değil olması için  
                claims: SetClaims(user,operationClaims),
                signingCredentials: signingCredentials
                );
            return jwt;
        }
        private  IEnumerable<Claim> SetClaims(User user,List<OperationClaim> operationClaims)
        {
            var claimsss = new List<Claim>();
            //claims.Add(new Claim("email", user.Email));
            claimsss.AddNameIdentifier(user.Id.ToString());
            claimsss.AddEmail(user.Email);
            claimsss.AddName($"{user.FirstName} {user.LastName}");
            claimsss.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claimsss;

        }
    }
}
