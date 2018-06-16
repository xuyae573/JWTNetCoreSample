using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
 
using IdentityAPI.Services;
using Microsoft.AspNetCore.Mvc;
using SimpleCommerce.Core.Domain;
using SimpleCommerce.Core.Dto;

namespace IdentityAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class TokenController : Controller
    {
        private IUserService _userService;
        private readonly TokenAuthConfiguration _configuration;
        public TokenController(IUserService userService, TokenAuthConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        /// <summary>
        /// To verify the userid and password and return the JWT
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns>user info with access token </returns>
        [HttpGet]
        [ActionName("auth")]
        public IActionResult Authenticate(string userId, string password)
        {
            var user = _userService.SignIn(new UserDto() {
                UserId = userId,
                Password = password
            });

            if (user!= null)
            {
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                identity.AddClaim(new Claim(ClaimTypes.Gender, user.Gender));

                //return Token
                var accessToken = CreateAccessToken(CreateJwtClaims(identity));

                var result = new GenericAPIResponse()
                {
                    Success = true,
                    Result = new AuthenticateResultModel()
                    {
                        AccessToken = accessToken,
                        UserId = user.UserId,
                        ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                    }
                };

                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

    }
}