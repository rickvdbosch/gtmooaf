using System;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Gtmooaf.Models;

namespace Gtmooaf
{
    public static class HttpTriggerReturnBinding
    {
        [FunctionName(nameof(HttpTriggerReturnBinding))]
        [return: Blob("data/{sys.randguid}.txt", Connection = "scs")]
        public static string Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] SomeCustomModel model,
            ILogger log)
        {
            return $"{model.Name} posted their info, if I'm not mistaking their age is currently '{CalculateAge(model.DateOfBirth)} years'.";
        }

        #region Private method

        private static int CalculateAge(DateTime birthDate)
        {
            var now = DateTime.Now;
            int age = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                age--;

            return age;
        }

        #endregion
    }
}