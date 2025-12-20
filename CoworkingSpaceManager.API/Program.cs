var builder = WebApplication.CreateBuilder(args);

/* service area */

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

app.UseHttpsRedirection();
app.MapGet("/", options =>
{
    options.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.MapControllers();
app.Run();