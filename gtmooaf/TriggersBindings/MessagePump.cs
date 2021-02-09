using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Gtmooaf.TriggersBindings
{
    public static class MessagePump
    {
        #region Constants

        private const char SPACE = ' ';

        #endregion

        [FunctionName(nameof(MessagePump))]
        public static async Task Run(
            [BlobTrigger("upload/{name}", Connection = "scs")] Stream addedBlob,
            [ServiceBus("process", Connection = "sbcs", EntityType = EntityType.Queue)]
                ICollector<string> queueCollector,
            ILogger log)
        {
            using (var reader = new StreamReader(addedBlob))
            {
                // The ServiceBus Output binding using an ICollector<T> which enables you to write a 
                // message to the queue/topic you bind to EACH TIME YOU CALL the Add() method!
                var words = (await reader.ReadToEndAsync()).Split(SPACE);
                Parallel.ForEach(words.Distinct(), (word) =>
                {
                    queueCollector.Add(word);
                });
            }
        }
    }
}