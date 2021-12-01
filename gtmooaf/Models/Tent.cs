using Microsoft.Azure.Cosmos.Table;

namespace Gtmooaf.Models
{
    public class Tent : TableEntity
    {
        #region Constructor

        public Tent() => PartitionKey = "betatalksLIVE-20201209";

        #endregion

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}