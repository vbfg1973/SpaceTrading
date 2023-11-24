using SpaceTrading.Production.Data;

namespace SpaceTrading.Production.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
            => services.AddDbContext<SpaceTradingContext>();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}