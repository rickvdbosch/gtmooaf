using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Gtmooaf.Interfaces;
using Gtmooaf.Models;

namespace Gtmooaf.DependencyInjection
{
    public class Greet02
    {
        #region Fields

        private readonly IGreeterService _greeterService;

        #endregion

        #region Constructors

        public Greet02(IGreeterService greeterService)
        {
            _greeterService = greeterService;
        }

        #endregion

        [FunctionName(nameof(Greet02))]
        public string Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] SomeCustomModel model,
            ILogger log) => _greeterService.Greet(model.Name);
    }
}