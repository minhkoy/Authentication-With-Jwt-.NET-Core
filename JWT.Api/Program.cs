using JWT.Infrastructure.ApiIO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using FluentValidation;
using JWT.Data;
using JWT.Data.Interfaces;
using JWT.Data.UnitOfWork;
using JWT.Helper.Config;
using JWT.Manager.JwtAuthentication.Request;
//using JWT.Manager.Validators;
using Microsoft.EntityFrameworkCore;
using JWT.Manager.RequestModelValidators;
using JWT.Manager.RequestValidators;
using Microsoft.OpenApi.Models;
using JWT.InternalServices;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyCorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(JWT.Manager.AssemblyReference.Assembly);
    //config.AddOpenBehavior(typeof(GenericBehavior<,>))
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtOption:Issuer"],
            ValidAudience = builder.Configuration["JwtOption:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOption:Key"]))
        };
    });
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "V1",
        Title = "JWT Test API",
        Description = "A demo API to test JWT Authentication"
    });
    s.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer. \r\n\r\n
                            Enter 'Bearer[SPACE]' and then your token in the textinput below. \r\n\r\n
                            Example: 'Bearer 123456defabc' ",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme,
            }
        },
        new string[] {}
        }
    };
    s.AddSecurityRequirement(securityRequirement);
});

var services = builder.Services;
services.AddDbContext<JwtDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr"), b => b.MigrationsAssembly("JWT.Data"));
});

services.AddInternalService();
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddValidators();

services.AddTransient<IUnitOfWork, UnitOfWork>();
services.Configure<JwtOption>(configuration.GetSection(nameof(JwtOption)));

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
