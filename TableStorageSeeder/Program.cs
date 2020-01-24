using System;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

using Gtmooaf.Models;

namespace TableStorageSeeder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference(nameof(Tent));
            var rnd = new Random();

            for (int i = 0; i < 250; i++)
            {
                var entity = new Tent { RowKey = $"20200124{rnd.Next(0, 60):00}", Id = i, Name = $"This is item nr. {i + 1}" };
                var insertOperation = TableOperation.InsertOrReplace(entity);
                await table.ExecuteAsync(insertOperation);
            }
        }
    }
}
