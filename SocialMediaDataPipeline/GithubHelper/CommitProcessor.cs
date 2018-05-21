using SocialMediaCommonHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubHelper
{
    public class CommitProcessor
    {
        string processorName;
        DateTime processDate;
        int jobRunId;
        long repoId;
        string commitSHA;
        string token;
        GithubCommonHelper gitHelper;
        AzureDataLakeHelper adlHelper = new AzureDataLakeHelper();

        public CommitProcessor(string message)
        {
            message = message.Replace(',', ';');
            processorName = message.Split(';')[0];
            processDate = Convert.ToDateTime(message.Split(';')[1]);
            jobRunId = Convert.ToInt32(message.Split(';')[2]);
            repoId = Convert.ToInt64(message.Split(';')[3]);
            commitSHA =message.Split(';')[4];
            token = message.Split(';')[5];
            gitHelper = new GithubCommonHelper(token);
        }
        public void GetContent()
        {           
            string commit = string.Empty;
            commit = gitHelper.GetCommit(repoId, commitSHA);
            if (!string.IsNullOrEmpty(commit))
                adlHelper.ConcurrentAppendFile("/SocialMedia/Github/" + processDate.ToString("yyyyMMddHH") + "/" + processorName , commit);
        }
    }
}
