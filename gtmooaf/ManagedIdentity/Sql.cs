using System.Data.SqlClient;
using System.Threading.Tasks;

using Azure.Identity;
using Azure.Core;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Gtmooaf.ManagedIdentity
{
    public static class Sql
    {
        #region Constants

        private const string CONNECTIONSTRING = "Server=tcp:<YOUR_SERVER_NAME>.database.windows.net,1433;Initial Catalog=<YOUR_DATABASE_NAME>;";
        private const string QUERY = "<YOUR_SQL_QUERY>";
        // TENANT_ID can both be the Directory ID (GUID) or the 'xxx.onmicrosoft.com' name of the tenant.
        private const string TENANT_ID = "<YOUR_TENANT_ID>";

        #endregion

        [FunctionName(nameof(Sql))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            using var connection = new SqlConnection(CONNECTIONSTRING);
            using var command = new SqlCommand(QUERY, connection);

            connection.AccessToken = (await new ManagedIdentityCredential().GetTokenAsync(new TokenRequestContext(new[] { "https://database.windows.net/" }))).Token;
            await connection.OpenAsync();
            var result = (await command.ExecuteScalarAsync()).ToString();

            return new OkObjectResult(result);
        }
    }
}