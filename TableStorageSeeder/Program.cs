using System;
using System.Threading.Tasks;

using Microsoft.Azure.Cosmos.Table;

using Gtmooaf.Models;

namespace TableStorageSeeder
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference(nameof(Tent));
            var rnd = new Random();

            for (int i = 0; i < 250; i++)
            {
                var entity = new Tent { 
                    RowKey = $"20211213{rnd.Next(0, 1000):000}", 
                    Name = $"This is item nr. {i + 1}",
                    Id = i
                };
                var insertOperation = TableOperation.InsertOrReplace(entity);
                await table.ExecuteAsync(insertOperation);
            }
        }
    }
}
