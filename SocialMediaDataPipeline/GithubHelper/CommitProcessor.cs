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
            try
            {
                gitHelper.CheckRateLimit();
                commit = gitHelper.GetCommit(repoId, commitSHA);
                if (!string.IsNullOrEmpty(commit))
                    adlHelper.ConcurrentAppendFile("/SocialMedia/Github/" + processDate.ToString("yyyyMMdd") + "/" + processorName, commit);
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
                    adlHelper.ConcurrentAppendFile("/SocialMedia/Github/" + processDate.ToString("yyyyMMdd") + "/" + processorName + "_deleted", string.Format("{0};{1}", repoId, commitSHA));
                }
                else if (errorMsg.Equals("Repository access blocked"))
                {
                    adlHelper.ConcurrentAppendFile("/SocialMedia/Github/" + processDate.ToString("yyyyMMdd") + "/" + processorName + "_blocked", string.Format("{0};{1}", repoId, commitSHA));
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
