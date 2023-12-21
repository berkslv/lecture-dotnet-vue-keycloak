using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Secured.API;
using Secured.API.Persistence;
using Secured.API.Persistence.Interfaces;

var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.ShowPII = true;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var myAllowedOrigins = "_myAllowedOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowedOrigins, policy =>
    {
        policy
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .Build();
    });
});

builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    options.UseInMemoryDatabase("libraryDatabase");
});

builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

builder.Services.AddWebServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(myAllowedOrigins);

// app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();