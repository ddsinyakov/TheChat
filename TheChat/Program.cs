
using Microsoft.EntityFrameworkCore;
using TheChat.Services.Database.RoleDao;
using TheChat.Services.DataBase;
using TheChat.Services.DataBase.UserDAO;
using TheChat.Services.Hash;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration; // access to configuration

// -------------------------------------------------------------------------------------------
// Add services to the container.
// -------------------------------------------------------------------------------------------

builder.Services.AddControllers();          // asp .net controllers
builder.Services.AddEndpointsApiExplorer(); // search for endpoints to show in swagger 
builder.Services.AddSwaggerGen();           // swagger interface 

builder.Services.AddScoped<IUserDao, UserDao>();
builder.Services.AddScoped<IRoleDao, RoleDao>();
builder.Services.AddSingleton<IHashService, Sha256HashService>();

builder.Services.AddDbContext<TheChatDbContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
});

// cross domen requests
// TODO: change accessibility onlu for frontend pages
builder.Services.AddCors();                 

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
