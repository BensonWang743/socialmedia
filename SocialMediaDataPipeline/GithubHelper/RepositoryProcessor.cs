﻿using SocialMediaCommonHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubHelper
{
    public class RepositoryProcessor
    {
        string processorName;
        DateTime processDate;
        int jobRunId;
        long repoId;
        string token;
        GithubCommonHelper gitHelper;
        AzureDataLakeHelper adlHelper = new AzureDataLakeHelper();

        public RepositoryProcessor(string message)
        {
            message = message.Replace(',', ';');
            processorName = message.Split(';')[0];
            processDate = Convert.ToDateTime(message.Split(';')[1]);
            jobRunId = Convert.ToInt32(message.Split(';')[2]);
            repoId =Convert.ToInt64(message.Split(';')[3]);
            token = message.Split(';')[5];
            gitHelper = new GithubCommonHelper(token);
        }
        public void GetContent()
        {
            string repository = string.Empty;
            repository = gitHelper.GetRepository(repoId);
            if (!string.IsNullOrEmpty(repository))
                adlHelper.ConcurrentAppendFile("/SocialMedia/Github/" + processDate.ToString("yyyyMMddHH") + "/" + processorName, repository);
        }
    }
}