using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Korepetynder.Data
{
	public static class Configurator
	{
		public static void ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
			services.AddDbContext<KorepetynderDbContext>(options =>
			{
				options.UseSqlServer(connectionString);
			});
        }
	}
}

