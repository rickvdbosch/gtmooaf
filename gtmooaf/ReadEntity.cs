using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

using Gtmooaf.Models;

namespace Gtmooaf
{
    public static class ReadEntity
    {
        [FunctionName("ReadEntity")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "entities/{id}")] HttpRequest request,
            [Table(nameof(Tent), "betatalksLIVE-20201209", "{id}")] Tent entity)
        {
            return new OkObjectResult(entity);
        }
    }
}
