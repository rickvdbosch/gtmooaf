using System;
using System.Threading.Tasks;

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Gtmooaf.ManagedIdentity
{
    public static class KeyVault
    {
        #region Constants

        // Name of the secret to get from Key Vault
        private const string SECRET_NAME = "Secret1";

        // The URL to the Key Vault to get the secret from
        private const string VAULT_URL = "https://gtmooaf-kv.vault.azure.net/";

        #endregion

        [FunctionName(nameof(KeyVault))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var kvc = new SecretClient(new Uri(VAULT_URL), new DefaultAzureCredential());

            var secret = await kvc.GetSecretAsync(SECRET_NAME);
            Console.WriteLine($"The value of the secret we got from Key Vault: {secret.Value}");

            return new OkObjectResult(secret.Value);
        }
    }
}
