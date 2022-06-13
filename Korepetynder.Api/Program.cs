using Korepetynder.Api.StartupExtensions;
using Korepetynder.Data;
using Korepetynder.Services;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwagger(builder.Configuration);
}

builder.Services.ConfigureDatabase(builder.Configuration.GetConnectionString("Korepetynder"));

builder.Services.ConfigureServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.AddPolicy("AngularSPA", builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    }

    if (builder.Environment.IsProduction())
    {
        options.AddPolicy("Main", builder =>
        {
            builder.WithOrigins("https://korepetynder.pl")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });

    }
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
app.UseRouting();

if (builder.Environment.IsDevelopment())
{
    app.UseCors("AngularSPA");
}
if (builder.Environment.IsProduction())
{
    app.UseCors("Main");
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
