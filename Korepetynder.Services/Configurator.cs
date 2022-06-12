using Azure.Storage.Blobs;
using Korepetynder.Services.Languages;
using Korepetynder.Services.Levels;
using Korepetynder.Services.Locations;
using Korepetynder.Services.Media;
using Korepetynder.Services.Students;
using Korepetynder.Services.Subjects;
using Korepetynder.Services.Tutors;
using Korepetynder.Services.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sieve.Services;

namespace Korepetynder.Services
{
    public static class Configurator
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISieveProcessor, SieveProcessor>();
            services.AddSingleton(new BlobServiceClient(
                configuration.GetValue<string>("BlobStorageConnectionString")));

            services.AddScoped<ISubjectsService, SubjectsService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITutorService, TutorService>();
            services.AddScoped<ILevelsService, LevelsService>();
            services.AddScoped<ILanguagesService, LanguagesService>();
            services.AddScoped<ILocationsService, LocationsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMediaService, MediaService>();
        }
    }
}

