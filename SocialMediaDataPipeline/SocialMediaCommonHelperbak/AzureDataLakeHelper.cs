﻿using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates; // Required only if you are using an Azure AD application created with certificates

using Microsoft.Rest;
using Microsoft.Rest.Azure.Authentication;
using Microsoft.Azure.DataLake.Store;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Azure.Management.DataLake.Store.Models;
using Microsoft.Azure.Management.DataLake.Store;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace SocialMediaCommonHelper
{
    public class AzureDataLakeHelper
    {
        string _adlsAccountName = "socialmediaadls.azuredatalakestore.net";
        AdlsClient client;
        public AzureDataLakeHelper()
        {
            string TENANT = "6c5ccde4-18b5-4936-ae88-995adc1f3c22";
            string CLIENTID = "0649e374-afa1-44e6-a061-c44944a156d1";
            Uri ARM_TOKEN_AUDIENCE = new System.Uri(@"https://management.core.windows.net/");
            Uri ADL_TOKEN_AUDIENCE = new System.Uri(@"https://datalake.azure.net/");
            string secret_key = "oVOdbXKU28RE5oH2symmWXp3sYOoG1geV8x2q4nrgm0=";
            var armCreds = GetCreds_SPI_SecretKey(TENANT, ARM_TOKEN_AUDIENCE, CLIENTID, secret_key);
            var adlCreds = GetCreds_SPI_SecretKey(TENANT, ADL_TOKEN_AUDIENCE, CLIENTID, secret_key);
            client = AdlsClient.CreateClient(_adlsAccountName, adlCreds); 
        }

        public void ConcurrentAppendFile(string fileName, string content)
        {
            byte[] textByteArray = Encoding.UTF8.GetBytes(string.Format("{0}\r\n",content));
            client.ConcurrentAppend(fileName, true, textByteArray, 0, textByteArray.Length);
        }

        private static ServiceClientCredentials GetCreds_SPI_SecretKey(string tenant, Uri tokenAudience, string clientId, string secretKey)
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
