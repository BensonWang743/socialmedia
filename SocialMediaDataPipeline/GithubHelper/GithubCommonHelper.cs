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

        public string GetCommit(long repoId, string commitSHA)
        {
            string commit = string.Empty;
            var commitTasks = client.Repository.Commit.Get(repoId,commitSHA);
            commitTasks.Wait();
            if (commitTasks.Result != null)
                commit = JsonConvert.SerializeObject(commitTasks.Result);
            return commit;
        }
        public string GetIssue(long repoId, int number)
        {
            string issue = string.Empty;
            var issueTasks = client.Issue.Get(repoId,number);
            issueTasks.Wait();
            if (issueTasks.Result != null)
                issue = JsonConvert.SerializeObject(issueTasks.Result);
            return issue;
        }

        public string GetUsers(string login)
        {
            string user = string.Empty;
            var userTasks = client.User.Get(login);
            userTasks.Wait();
            if (userTasks.Result != null)
                user = JsonConvert.SerializeObject(userTasks.Result);
            return user;
        }

        public string GetRepository(long repoId)
        {
            string repository = string.Empty;
            var repoTasks = client.Repository.Get(repoId);
            repoTasks.Wait();
            if (repoTasks.Result != null)
                repository = JsonConvert.SerializeObject(repoTasks.Result);
            return repository;
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

        public int CheckRateLimit(string token)
        {
            int remain = 0;
            var rate = client.Miscellaneous.GetRateLimits();
            rate.Wait();

            if (rate.Result != null)
            {
                remain = rate.Result.Rate.Remaining;
            }
            return remain;
        }
    }
}
