

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using JiraProject.Entities.Entities;
using JiraProject.Entities.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JiraProject.Business.Security.JWT
{
    public class TokenHelper
    {
        public IConfiguration Configuration;
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public TokenHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("JwtConfiguration").Get<TokenOptions>();
        }



        public AccessToken CreateToken(User user, List<Role> roles)
        {
            _accessTokenExpiration = DateTime.Now.AddDays(_tokenOptions.AccessTokenExpiration);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var jwtToken = CreateJwtSecurityToken(_tokenOptions, user, signInCredentials, roles);
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtTokenHandler.WriteToken(jwtToken);

            return new AccessToken()
            {
                Token = token,
                Expiration = _accessTokenExpiration,
                UserId = user.Id,
                Email = user.Email,
                Name = $"{user.Name} {user.LastName}",
                Picture = user.Picture,
                UserRoles = roles.Select(x=> x.RoleName.ToString()).ToList()
            };



        }




        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signInCredentials, List<Role> roles)
        {
            return new JwtSecurityToken(
                    issuer: tokenOptions.Issuer,
                    audience: tokenOptions.Audience,
                    expires: _accessTokenExpiration,
                    notBefore: DateTime.Now,
                    claims : SetClaims(user, roles),
                    signingCredentials: signInCredentials);
        }




        private List<Claim> SetClaims(User user, List<Role> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.Name} {user.LastName}"),
                new Claim("user-picture", user.Picture),
                new Claim(ClaimTypes.Gender, user.Gender.ToString()),
            };

            claims.AddRoles(roles);
            return claims;


        }
    }
}