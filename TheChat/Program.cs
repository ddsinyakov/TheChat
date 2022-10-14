
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration; // access to configuration

// -------------------------------------------------------------------------------------------
// Add services to the container.
// -------------------------------------------------------------------------------------------

builder.Services.AddControllers();          // asp .net controllers
builder.Services.AddEndpointsApiExplorer(); // search for endpoints to show in swagger 
builder.Services.AddSwaggerGen();           // swagger interface 

// cross domen requests
// TODO: change accessibility onlu for frontend pages
builder.Services.AddCors();                 

var app = builder.Build();




// -------------------------------------------------------------------------------------------
// Configure the HTTP request pipeline.
//-------------------------------------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyOrigin());
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
