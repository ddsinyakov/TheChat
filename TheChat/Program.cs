
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TheChat.Services.Authentication;
using TheChat.Services.Database.RoleDao;
using TheChat.Services.DataBase;
using TheChat.Services.DataBase.UserDAO;
using TheChat.Services.Hash;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration; // access to configuration

// -------------------------------------------------------------------------------------------
// Add services to the container.
// -------------------------------------------------------------------------------------------

builder.AddJwtAuthenticationSettings();     // Extension method to congigure jwt authentication 

builder.Services.AddControllers();          // Asp .net controllers
builder.Services.AddEndpointsApiExplorer(); // Search for endpoints to show in swagger 
builder.Services.AddSwaggerGen();           // Swagger interface 
builder.Services.AddHttpContextAccessor();  // Allows extra services access to HttpContext
builder.Services.AddCors();                 // Cross domen requests

builder.Services.AddScoped<IUserDao, UserDao>();
builder.Services.AddScoped<IRoleDao, RoleDao>();
builder.Services.AddSingleton<IHashService, Sha256HashService>();
builder.Services.AddSingleton<IAuthenticationService, JwtAuthenticationService>();

builder.Services.AddDbContext<TheChatDbContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
});            

var app = builder.Build();


// -------------------------------------------------------------------------------------------
// Configure the HTTP request pipeline.
// -------------------------------------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyOrigin());

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
