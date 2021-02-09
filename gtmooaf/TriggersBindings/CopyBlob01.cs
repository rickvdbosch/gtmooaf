using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;

namespace Gtmooaf.TriggersBindings
{
    public static class CopyBlob01
    {
        [FunctionName(nameof(CopyBlob01))]
        [FixedDelayRetry(5, "00:00:10")]
        public static async Task Run(
            [BlobTrigger("upload/{name}", Connection = "scs")] Stream addedBlob, 
            string name, ILogger log)
        {
            // The below code is the 'old-fashioned' way of uploading a file to storage:
            // - Connect to a Storage Account
            // - Create a BlobClient
            // - Get a reference to a container (and create it if it doesn't exist)
            // - Get a reference to a blob
            // - Upload the file
            var connectionString = Environment.GetEnvironmentVariable("scs", EnvironmentVariableTarget.Process);
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("copied");
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(name);

            await blob.UploadFromStreamAsync(addedBlob);
        }
    }
}