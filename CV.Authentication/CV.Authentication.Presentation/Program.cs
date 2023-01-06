using CV.Authentication.AccessData;
using CV.Authentication.AccessData.Interfaces;
using CV.Authentication.AccessData.Repositories;
using CV.Authentication.Application.Handlers;
using CV.Authentication.Application.Interfaces;
using CV.Authentication.Application.Services;
using CV.Authentication.Domain.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Email
var appEmailSettingsSection = builder.Configuration.GetSection("EmailConfig");
builder.Services.Configure<EmailConfig>(appEmailSettingsSection);
// JWT
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<JwtConfig>(appSettingsSection);

var appSettings = appSettingsSection.Get<JwtConfig>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(d =>
{
    d.RequireHttpsMetadata = false;
    d.SaveToken = true;
    d.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});

// Db connection for Authentication
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Dependency Injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserHandler, UserHandler>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "corspolicy",
        build =>
        {
            build.WithOrigins("http://127.0.0.1:5500").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
        );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
