using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos.Table;

using Gtmooaf.Models;

namespace Gtmooaf
{
    public static class ReadTable
    {
        [FunctionName("ReadTable")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "entities")] HttpRequest request,
            [Table(nameof(Tent))] CloudTable cloudTable)
        {
            var entities = await GetEntitiesFromTable<Tent>(cloudTable, "RHH-20211213");
            return new OkObjectResult(entities);
        }

        #region Private methods

        private static async Task<List<T>> GetEntitiesFromTable<T>(CloudTable table, string partitionKey) where T : ITableEntity, new()
        {
            TableQuerySegment<T> querySegment = null;
            var entities = new List<T>();
            var query = new TableQuery<T>()
                .Where(
                TableQuery.GenerateFilterCondition(nameof(ITableEntity.PartitionKey), 
                QueryComparisons.Equal, 
                partitionKey));

            do
            {
                querySegment = await table.ExecuteQuerySegmentedAsync(query, querySegment?.ContinuationToken);
                entities.AddRange(querySegment.Results);
            } while (querySegment.ContinuationToken != null);

            return entities;
        }

        #endregion
    }
}
