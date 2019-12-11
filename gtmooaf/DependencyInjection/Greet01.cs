using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Gtmooaf.Models;
using Gtmooaf.Services;

namespace Gtmooaf.DependencyInjection
{
    public static class Greet01
    {
        [FunctionName(nameof(Greet01))]
        public static string Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] SomeCustomModel model,
            ILogger log)
        {
            var greeterService = new GreeterService();
            return greeterService.Greet(model.Name);
        }
    }
}