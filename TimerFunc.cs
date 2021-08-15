using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;

namespace azure_functions_ms_identity_web
{
    public class TimerFunc
    {
        private readonly ITokenAcquisition tokenAcquisition;

        public TimerFunc(ITokenAcquisition tokenAcquisition)
        {
            this.tokenAcquisition = tokenAcquisition;
        }

        [FunctionName("TimerFunc")]
        public async Task RunAsync([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                var token = await tokenAcquisition.GetAccessTokenForAppAsync("https://localhost:44349/.default", authenticationScheme: Constants.Bearer);

                log.LogInformation("Token: {Token}", token);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Failed to acquire token");
            }
        }
    }
}
