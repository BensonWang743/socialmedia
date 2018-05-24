using SocialMediaCommonHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubHelper
{
    public class UserProcessor
    {
        string processorName;
        DateTime processDate;
        int jobRunId;
        string login;
        string token;
        GithubCommonHelper gitHelper;
        AzureDataLakeHelper adlHelper = new AzureDataLakeHelper();

        public UserProcessor(string message)
        {
            processorName = message.Split(';')[0];
            processDate = Convert.ToDateTime(message.Split(';')[1]);
            jobRunId = Convert.ToInt32(message.Split(';')[2]);
            login = message.Split(';')[3];
            token = message.Split(';')[5];
            gitHelper = new GithubCommonHelper(token);
        }
        public void GetContent()
        {
            string user = string.Empty;
            try
            {
                gitHelper.CheckRateLimit();
                user = gitHelper.GetUsers(login);
                if (!string.IsNullOrEmpty(user))
                    adlHelper.ConcurrentAppendFile("/SocialMedia/Github/" + processDate.ToString("yyyyMMddHH") + "/" + processorName, user);
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
                    adlHelper.ConcurrentAppendFile("/SocialMedia/Github/" + processDate.ToString("yyyyMMddHH") + "/" + processorName + "_deleted", login);
                }
                else if (errorMsg.Equals("Repository access blocked"))
                {
                    adlHelper.ConcurrentAppendFile("/SocialMedia/Github/" + processDate.ToString("yyyyMMddHH") + "/" + processorName + "_blocked", login);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
