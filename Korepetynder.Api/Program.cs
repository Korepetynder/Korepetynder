using Korepetynder.Api.StartupExtensions;
using Korepetynder.Data;
using Korepetynder.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.ConfigureDatabase(builder.Configuration.GetConnectionString("Korepetynder"));

builder.Services.ConfigureServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular SPA", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
