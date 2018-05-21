using Microsoft.Azure.Management.DataLake.Analytics;
using Microsoft.Azure.Management.DataLake.Analytics.Models;
using Microsoft.Azure.Management.DataLake.Store;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADLASample
{
    class Program
    {
        private static DataLakeAnalyticsAccountManagementClient _adlaClient;
        private static DataLakeAnalyticsJobManagementClient _adlaJobClient;
        private static DataLakeAnalyticsCatalogManagementClient _adlaCatalogClient;
        private static DataLakeStoreAccountManagementClient _adlsClient;
        private static DataLakeStoreFileSystemManagementClient _adlsFileSystemClient;

        private static string _adlaAccountName;
        private static string _adlsAccountName;
        private static string _resourceGroupName;
        private static string _location;
        static void Main(string[] args)
        {
            _adlsAccountName = "socialmediaadls"; // TODO: Replace this value with the name for a created Store account.
            _adlaAccountName = "socialmediaadla"; // TODO: Replace this value with the name for a created Analytics account.
            string localFolderPath = @"D:\GitHubRepos\bensonwang743\socialmedia\SocialMediaDataPipeline\ADLASample\usql\"; // TODO: Make sure this exists and contains the U-SQL script.

            // Authenticate the user
            // For more information about applications and instructions on how to get a client ID, see: 
            // https://azure.microsoft.com/en-us/documentation/articles/resource-group-create-service-principal-portal/
            var tokenCreds = AuthenticateUser("common", "https://management.core.windows.net/",
                "0649e374-afa1-44e6-a061-c44944a156d1", new Uri("http://www.microsoft.com")); // TODO: Replace applicaion id and redirect url values.

            SetupClients(tokenCreds, "subscription idc3aeada3-abaf-4c38-8bc9-9504907f045c"); // TODO: Replace subscription value.

            // Run sample scenarios

            // Transfer the source file from a public Azure Blob container to Data Lake Store.


            // Submit the job
            string jobId = SubmitJobByPath(localFolderPath + "SampleUSQLScript.txt", "My First ADLA Job");

            // Wait for job completion

        }
        public static TokenCredentials AuthenticateUser(string tenantId, string resource, string appClientId, Uri appRedirectUri, string userId = "")
        {
            var authContext = new AuthenticationContext("https://login.microsoftonline.com/" + tenantId);

            var tokenAuthResult = authContext.AcquireTokenAsync(resource, appClientId, appRedirectUri, new PlatformParameters(PromptBehavior.Auto),
                 UserIdentifier.AnyUser).Result;

            return new TokenCredentials(tokenAuthResult.AccessToken);
        }

        public static void SetupClients(TokenCredentials tokenCreds, string subscriptionId)
        {
            _adlaClient = new DataLakeAnalyticsAccountManagementClient(tokenCreds) { SubscriptionId = subscriptionId };

            _adlaJobClient = new DataLakeAnalyticsJobManagementClient(tokenCreds);

            _adlaCatalogClient = new DataLakeAnalyticsCatalogManagementClient(tokenCreds);

            _adlsClient = new DataLakeStoreAccountManagementClient(tokenCreds) { SubscriptionId = subscriptionId };

            _adlsFileSystemClient = new DataLakeStoreFileSystemManagementClient(tokenCreds);
        }

        public static string SubmitJobByPath(string scriptPath, string jobName)
        {
            var script = File.ReadAllText(scriptPath);
            var jobId = Guid.NewGuid();
            var properties = new USqlJobProperties(script);
            var parameters = new JobInformation(jobName, JobType.USql, properties, priority: 1000, degreeOfParallelism: 1);
            var jobInfo = _adlaJobClient.Job.Create(_adlaAccountName, jobId, parameters);
            return jobId.ToString();
        }

    }
}
