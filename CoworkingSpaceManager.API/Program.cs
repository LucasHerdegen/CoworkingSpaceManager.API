using System.Text;
using CoworkingSpaceManager.API.DTOs;
using CoworkingSpaceManager.API.Mappers;
using CoworkingSpaceManager.API.Models;
using CoworkingSpaceManager.API.Repository;
using CoworkingSpaceManager.API.Services;
using CoworkingSpaceManager.API.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

/* service area */

// Database connection
builder.Services.AddDbContext<CoworkingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContextConnection"));
});

// Identity & JWT
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
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
builder.Services.AddScoped<IValidator<SpacePutDto>, SpacePutValidator>();
builder.Services.AddScoped<IValidator<BookingPostDto>, BookingPostValidator>();

// mappers
builder.Services.AddAutoMapper(typeof(MapperProfile));

// services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISpaceService, SpaceService>();
builder.Services.AddScoped<IBookingService, BookingService>();

// Repositorios
builder.Services.AddScoped<IRepository<Space>, SpaceRepository>();
builder.Services.AddScoped<IRepository<Booking>, BookingRepository>();

// 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Coworking API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

/* middleware area */

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al crear los roles iniciales.");
    }
}

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