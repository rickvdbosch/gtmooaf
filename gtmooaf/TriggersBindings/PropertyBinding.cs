using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Gtmooaf.Models;

namespace Gtmooaf.TriggersBindings
{
    public static class PropertyBinding
    {
        [FunctionName(nameof(PropertyBinding))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] SomeCustomModel model,
            [Blob("properties/{Identifier}.txt", FileAccess.Write, Connection = "scs")] Stream stream,
            ILogger log)
        {
            using var writer = new StreamWriter(stream);
            await writer.WriteLineAsync($"Name:\t{model.Name}");
            await writer.WriteLineAsync($"DoB:\t{model.DateOfBirth.ToString("dd-MM-yyyy")}");

            return new OkResult();
        }
    }
}
