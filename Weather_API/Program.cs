using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Weather_API.Core.Interface;
using Weather_API.Core.Services;
using Weather_API.Core.Utilities;
using Weather_API.Domain.Models;
using Weather_API.Infrastructure;
using Weather_API.Infrastructure.Repositoy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("Jwt:Token").Value)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration.GetSection("Issuer").Value,
            ValidAudience = builder.Configuration.GetSection("Audience").Value,
        };
    });
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddUserManager<UserManager<AppUser>>()
    .AddEntityFrameworkStores<Web_APIDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<Web_APIDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddAutoMapper(typeof(Web_APIProfile));
builder.Services.AddScoped<IWeatherInformation, WeatherInformation>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
