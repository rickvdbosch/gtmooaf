using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Gtmooaf.Models
{
    public class Tent : TableEntity
    {
        #region

        public Tent() : base("azurelowlands", DateTime.Now.ToString("yyyyMMddHHmm"))
        {
        }

        #endregion

        public int Id { get; set; }

        public string Name { get; set; }
    }
}