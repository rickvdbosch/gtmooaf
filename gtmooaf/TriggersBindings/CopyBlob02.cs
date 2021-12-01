using System.IO;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Gtmooaf.TriggersBindings
{
    public static class CopyBlob02
    {
        [FunctionName(nameof(CopyBlob02))]
        public static async Task Run(
            [BlobTrigger("upload/{name}", Connection = "scs")] Stream addedBlob,
            [Blob("copied/{name}", FileAccess.Write, Connection = "scs")] Stream stream,
            ILogger log)
        {
            await addedBlob.CopyToAsync(stream);
        }
    }
}