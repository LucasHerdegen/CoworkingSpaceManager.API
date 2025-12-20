using CoworkingSpaceManager.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/* service area */

builder.Services.AddDbContext<CoworkingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContextConnection"));
});

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

app.MapGet("/", options =>
{
    options.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.MapControllers();
app.Run();