using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TheChat.Services.Authentication
{
    public static class JwtAuthenticationSettings
    {
        public static void AddJwtAuthenticationSettings(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                { 
                    options.Events = new JwtBearerEvents
                    {
                        // Gets token from cookie
                        OnMessageReceived = context =>
                        {
                            if (context.HttpContext.Request.Cookies.ContainsKey("jwtToken"))
                                context.Token = context.HttpContext.Request.Cookies["jwtToken"];

                            return Task.CompletedTask;
                        },

                        //OnTokenValidated = context =>
                        //{
                        //    context.SecurityToken.ValidTo

                        //    return Task.CompletedTask;
                        //}
                    };

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(builder.Configuration.GetSection("AppSettings:Token:SecretKey").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}
