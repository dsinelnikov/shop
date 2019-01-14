using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Shop.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IAuthorizationConfiguration GetAuthorization(this IConfiguration configuration)
        {
            return new AuthorizationConfiguration(configuration.GetSection("Authorization"));
        }

        public interface IAuthorizationConfiguration
        {
            string Issuer { get; }
            string Audience { get; }
            SymmetricSecurityKey SecurityKey { get; }
            TimeSpan LifeTime { get; }
        }

        private class AuthorizationConfiguration : IAuthorizationConfiguration
        {
            private readonly IConfigurationSection _authorizationSection;

            public string Issuer => _authorizationSection["Issuer"];
            public string Audience => _authorizationSection["Audience"];
            public SymmetricSecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authorizationSection["Key"]));
            public TimeSpan LifeTime => TimeSpan.Parse(_authorizationSection["LifeTime"]);

            public AuthorizationConfiguration(IConfigurationSection authorizationSection)
            {
                _authorizationSection = authorizationSection;
            }
        }
    }
}