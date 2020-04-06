using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;

using Gtmooaf.Models;

namespace Gtmooaf.TriggersBindings
{
    public static class TableInput
    {
        [FunctionName(nameof(TableInput))]
        public static async Task Run(
            [QueueTrigger("trigger", Connection = "scs")] string message,
            [Table(nameof(Tent))] CloudTable table)
        {
            var entities = await GetEntitiesFromTable(table);

            foreach (var entity in entities)
            {
                log.LogInformation($"{entity.Id}:\t{entity.Name}");

            }
        }

        #region Private methods

        private static async Task<List<Tent>> GetEntitiesFromTable(CloudTable table)
        {
            TableQuerySegment<Tent> querySegment = null;
            var entities = new List<Tent>();
            var query = new TableQuery<Tent>().Where(TableQuery.GenerateFilterCondition(nameof(ITableEntity.PartitionKey), QueryComparisons.Equal, "azurelowlands"));

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