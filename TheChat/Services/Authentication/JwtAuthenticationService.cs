using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TheChat.Features;
using TheChat.Models.Entities;

namespace TheChat.Services.Authentication
{
    public class JwtAuthenticationService : IAuthenticationService 
    {

        IConfiguration _configuration { get; init; }
        IHttpContextAccessor _httpContextAccessor { get; init; }

        public JwtAuthenticationService(
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Generate JWT token using secret key and payload made out of user login and role
        /// </summary>
        /// <returns>JWT token</returns>
        /// <exception cref="ArgumentNullException">Throws if forUser or its Login or Role is null</exception>
        public String GenerateToken(User forUser)
        {
            if (forUser.Login is null)
                throw new ArgumentNullException(nameof(forUser.Login));

            if (forUser.Role is null)
                throw new ArgumentNullException(nameof(forUser.Role));

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, forUser.Login),
                new Claim(ClaimTypes.Role, forUser.Role.RoleName)
            };

            // Build secret key using key from configuration file
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token:SecretKey").Value));

            // Create hashed credentials for signing payload
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Get token life time from configuraton file
            Int32 lifeTime = _configuration.GetSection("AppSettings:Token").GetValue<int>("LifeTime");

            // Combine payload with signing credentials to form token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(lifeTime),
                signingCredentials: creds);

            // Build token
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        /// <summary>
        /// Authenticate user with adding JWT token to it's cookies
        /// </summary>
        /// <exception cref="Exception">The method called without existing HttpContext</exception>
        public void Authenticate(User toAuthenticate)
        {
            if (_httpContextAccessor.HttpContext is null)
                throw new Exception("Method call out of context. HttpContext is null");

            if (toAuthenticate is null)
                throw new ArgumentNullException(nameof(toAuthenticate));

            String token = GenerateToken(toAuthenticate);

            // Create options that describes cookie
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(1)
            };

            // Add new cookie with JWT token
            _httpContextAccessor.HttpContext.Response.Cookies.Append(
                key: "jwtToken",
                value: token,
                options: cookieOptions
            );
        }

        /// <summary>
        /// Remove JWT token from cookies if is exist
        /// </summary>
        /// <exception cref="Exception">The method called without existing HttpContext</exception>
        public void LogOut()
        { 
            if (_httpContextAccessor.HttpContext is null)
                throw new Exception("Method call out of context. HttpContext is null");

            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("jwtToken"))
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("jwtToken");
            }
        }
    }
}
