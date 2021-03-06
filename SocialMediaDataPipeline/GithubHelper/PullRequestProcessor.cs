﻿using SocialMediaCommonHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubHelper
{
    public class PullRequestProcessor
    {
        string processorName;
        DateTime processDate;
        int jobRunId;
        long repoId;
        int number;
        string token;
        GithubCommonHelper gitHelper;
        AzureDataLakeHelper adlHelper = new AzureDataLakeHelper();

        public PullRequestProcessor(string message)
        {
            message=message.Replace(',',';');
            processorName=message.Split(';')[0];
            processDate=Convert.ToDateTime(message.Split(';')[1]);
            jobRunId=Convert.ToInt32(message.Split(';')[2]);
            repoId=Convert.ToInt64(message.Split(';')[3]); 
            number=Convert.ToInt32(message.Split(';')[4]);
            token= message.Split(';')[5];
            gitHelper = new GithubCommonHelper(token);
        }
        public void GetContent()
        {
            string changedFiles = string.Empty;
            string pulls = string.Empty;
            try
            {
                gitHelper.CheckRateLimit();
                pulls = gitHelper.GetPullRequest(repoId, number, out changedFiles);
                if (!string.IsNullOrEmpty(changedFiles))
                    adlHelper.ConcurrentAppendFile("/SocialMedia/Github/" + processDate.ToString("yyyyMMdd") + "/" + processorName + "Files", changedFiles);
                if (!string.IsNullOrEmpty(pulls))
                    adlHelper.ConcurrentAppendFile("/SocialMedia/Github/" + processDate.ToString("yyyyMMdd") + "/" + processorName, pulls);
            }
            catch (Exception e)
            {
                string errorMsg = string.Empty;
                if (e.InnerException != null)
                    errorMsg = e.GetBaseException().Message;
                else
                    errorMsg = e.Message;
                if (errorMsg.Equals("Not Found"))
                {
                    adlHelper.ConcurrentAppendFile("/SocialMedia/Github/" + processDate.ToString("yyyyMMdd") + "/" + processorName + "_deleted", string.Format("{0};{1}",repoId, number));
                }
                else if (errorMsg.Equals("Repository access blocked"))
                {
                    adlHelper.ConcurrentAppendFile("/SocialMedia/Github/" + processDate.ToString("yyyyMMdd") + "/" + processorName + "_blocked", string.Format("{0};{1}", repoId, number));
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
