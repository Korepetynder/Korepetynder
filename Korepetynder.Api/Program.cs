using Korepetynder.Api.StartupExtensions;
using Korepetynder.Data;
using Korepetynder.Services;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(builder.Configuration);

builder.Services.ConfigureDatabase(builder.Configuration.GetConnectionString("Korepetynder"));

builder.Services.ConfigureServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularSPA", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "AzureAdB2C");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ConfigureSwagger(app.Configuration);
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseCors("AngularSPA");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
