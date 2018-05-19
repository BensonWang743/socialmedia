using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GithubHelper
{
    public class GithubCommonHelper
    {
        GitHubClient client;
        public GithubCommonHelper(string token)
        {
            var tokenAuth = new Credentials(token);
            client = new GitHubClient(new ProductHeaderValue("skyeye"));
            client.Credentials = tokenAuth;
        }

        public string GetPullRequest(long repoId,int prNum,out string file)
        {
            string pullRequest = string.Empty;
            var prTasks = client.PullRequest.Get(repoId, prNum);
            prTasks.Wait();
            if(prTasks.Result!=null)
                pullRequest = JsonConvert.SerializeObject(prTasks.Result);
            var prFileTasks = client.PullRequest.Files(repoId, prNum);
            prFileTasks.Wait();
            if (prFileTasks.Result != null)
                file = JsonConvert.SerializeObject(prFileTasks.Result);
            else
                file = string.Empty;
            return pullRequest;
        }

        public void CheckRateLimit()
        {
            var rate = client.Miscellaneous.GetRateLimits();
            rate.Wait();

            if (rate.Result != null && rate.Result.Rate.Remaining < 50)
            {
                var waitTime = (int)(rate.Result.Rate.Reset.UtcDateTime - DateTime.UtcNow).TotalMilliseconds;

                if (waitTime > 0)
                    System.Threading.Thread.Sleep(waitTime + 30000);
            }

        }
    }
}
