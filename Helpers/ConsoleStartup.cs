using LoggingAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace LoggingAPI.Helpers
{
    public class ConsoleStartup
    {
        public ConsoleStartup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LoggingContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("LoggingDb"));
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}