using Microsoft.Azure.Management.DataLake.Analytics;
using Microsoft.Azure.Management.DataLake.Analytics.Models;
using Microsoft.Azure.Management.DataLake.Store;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.Rest.Azure.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ADLASample
{
    class Program
    {
       // private static DataLakeAnalyticsJobManagementClient _adlaJobClient;
        static void Main(string[] args)
        {
            string secret_key = "oVOdbXKU28RE5oH2symmWXp3sYOoG1geV8x2q4nrgm0=";
            string TENANT = "6c5ccde4-18b5-4936-ae88-995adc1f3c22";
            string CLIENTID = "0649e374-afa1-44e6-a061-c44944a156d1";
            string  _adlaAccountName = "socialmediaadla";
            Uri ARM_TOKEN_AUDIENCE = new System.Uri(@"https://management.core.windows.net/");
            Uri ADL_TOKEN_AUDIENCE = new System.Uri(@"https://datalake.azure.net/");

            var armCreds = GetCreds_SPI_SecretKey(TENANT, ARM_TOKEN_AUDIENCE, CLIENTID, secret_key);
            var adlCreds = GetCreds_SPI_SecretKey(TENANT, ADL_TOKEN_AUDIENCE, CLIENTID, secret_key);
            DataLakeAnalyticsJobManagementClient _adlaJobClient = new DataLakeAnalyticsJobManagementClient(armCreds);
            var jobId = Guid.NewGuid();
            string scriptPath = @"C:\GitRepo\bensonwang743\socialmedia\SocialMediaDataPipeline\ADLASample\usql\SampleUSQLScript.txt";
            var script = File.ReadAllText(scriptPath);
            var properties = new USqlJobProperties(script);
            var parameters = new JobInformation("test", JobType.USql, properties);
            var jobInfo = _adlaJobClient.Job.Create(_adlaAccountName,jobId, parameters);
        }
        private static ServiceClientCredentials GetCreds_SPI_SecretKey(
   string tenant,
   Uri tokenAudience,
   string clientId,
   string secretKey)
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

            var serviceSettings = ActiveDirectoryServiceSettings.Azure;
            serviceSettings.TokenAudience = tokenAudience;

            var creds = ApplicationTokenProvider.LoginSilentAsync(
             tenant,
             clientId,
             secretKey,
             serviceSettings).GetAwaiter().GetResult();
            return creds;
        }
    }
}
