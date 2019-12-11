using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

using Gtmooaf;
using Gtmooaf.Interfaces;
using Gtmooaf.Services;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Gtmooaf
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IGreeterService, GreeterService>();
        }
    }
}