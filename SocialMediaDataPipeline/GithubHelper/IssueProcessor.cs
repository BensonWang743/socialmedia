using SocialMediaCommonHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubHelper
{
    public class IssueProcessor
    {
        string processorName;
        DateTime processDate;
        int jobRunId;
        long repoId;
        int number;
        string token;
        GithubCommonHelper gitHelper;
        AzureDataLakeHelper adlHelper = new AzureDataLakeHelper();

        public IssueProcessor(string message)
        {
            message = message.Replace(',', ';');
            processorName = message.Split(';')[0];
            processDate = Convert.ToDateTime(message.Split(';')[1]);
            jobRunId = Convert.ToInt32(message.Split(';')[2]);
            repoId = Convert.ToInt64(message.Split(';')[3]);
            number =Convert.ToInt32(message.Split(';')[4]);
            token = message.Split(';')[5];
            gitHelper = new GithubCommonHelper(token);
        }
        public void GetContent()
        {
            string issue = string.Empty;
            issue = gitHelper.GetIssue(repoId, number);
            if (!string.IsNullOrEmpty(issue))
                adlHelper.ConcurrentAppendFile("/SocialMedia/Github/" + processDate.ToString("yyyyMMddHH") + "/" + processorName, issue);
        }
    }
}
