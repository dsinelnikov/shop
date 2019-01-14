using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Shop.Api.Extensions;
using Shop.Services.Interfaces;

namespace Shop.Api.Controllers
{
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationService _authenticationService;

        public AuthorizationController(IConfiguration configuration, IAuthenticationService authenticationService)
        {
            _configuration = configuration;
            _authenticationService = authenticationService;
        }

        [HttpPost("/token")]
        public async Task<IActionResult> Token()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];

            var identity = await GetIdentity(username, password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                return new Shop.Api.Results.BadRequestResult("Invalid username or password.");
            }
            
            var now = DateTime.UtcNow;
            var authorization = _configuration.GetAuthorization();
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: authorization.Issuer,
                    audience: authorization.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(authorization.LifeTime),
                    signingCredentials: new SigningCredentials(authorization.SecurityKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Ok(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            var user = await _authenticationService.GetUser(email, password);
            if(user == null)
            {
                return null;
            }

            var claims = user.Roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r.Name))
                .ToList();
            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, email));
            claims.Add(new Claim(ClaimsIdentityConsts.PersonIdClaimType, user.PersonId.ToString()));

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}