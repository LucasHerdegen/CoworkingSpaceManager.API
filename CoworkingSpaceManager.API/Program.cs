using System.Text;
using CoworkingSpaceManager.API.DTOs;
using CoworkingSpaceManager.API.Mappers;
using CoworkingSpaceManager.API.Models;
using CoworkingSpaceManager.API.Services;
using CoworkingSpaceManager.API.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

/* service area */

// Database connection
builder.Services.AddDbContext<CoworkingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContextConnection"));
});

// Identity & JWT
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<CoworkingContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// builder.Services.AddScoped<UserManager<ApplicationUser>>();
// builder.Services.AddScoped<SignInManager<ApplicationUser>>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]!)),
        ClockSkew = TimeSpan.Zero
    };
});

// validators
builder.Services.AddScoped<IValidator<RegisterDto>, RegisterValidator>();
builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();
builder.Services.AddScoped<IValidator<SpacePostDto>, SpacePostValidator>();

// mappers
builder.Services.AddAutoMapper(typeof(MapperProfile));

// services
builder.Services.AddScoped<IUserService, UserService>();

// 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

/* middleware area */

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", options =>
{
    options.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.MapControllers();
app.Run();