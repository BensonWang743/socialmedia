using GithubHelper.DataModel;
using Newtonsoft.Json;
using SocialMediaCommonHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GithubHelper
{
    public class GithubArchieveFileProcessor
    {

        ServiceBusHelper sbHelper = new ServiceBusHelper("githubqueue","Endpoint=sb://socialmediasb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Wi198MbCAvirOEjaT6MpZahWifihZFc6GMtm30icsb0=");
        Random random = new Random();
        string[] tokens = new string[] {
            ""
        };


        public void GitHubArchiveParser(DateTime processDate, int jobRunId)
        {
            Uri url = new Uri(string.Format(@"http://data.githubarchive.org/{0}-{1:D2}-{2:D2}-{3}.json.gz", processDate.Year, processDate.Month, processDate.Day, processDate.Hour));
            WebResponse response = null;
            //var request = HttpWebRequest.Create(url) as HttpWebRequest;
            string str = string.Empty;
            CommonHelper.RetryAndIgnore(() =>
            {
                var request = HttpWebRequest.Create(url) as HttpWebRequest;
                response = request.GetResponse();
            }, numRetries: 2, retryTimeout: 5);

            //using (response = request.GetResponse())
            //{
                //response = request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    if (response.ContentLength > int.MaxValue)
                    {
                        throw new ApplicationException("content too large");
                    }
                    using (var zipStream = new GZipStream(stream, CompressionMode.Decompress))
                    {
                        using (var streamReader = new StreamReader(zipStream))
                        {

                            while (str != null)
                            {
                                str = streamReader.ReadLine();
                                if (!string.IsNullOrEmpty(str))
                                {
                                    using (StringReader readerstr = new StringReader(str))
                                    {
                                        using (JsonTextReader jsReader = new JsonTextReader(readerstr))
                                        {
                                            JsonSerializer jSearial = new JsonSerializer();
                                            var gitHubArchiveEvent = jSearial.Deserialize<GithubArchiveModel>(jsReader);

                                            switch (gitHubArchiveEvent.type)
                                            {
                                                case "PullRequestEvent":
                                                    using (StringReader payLoadReader = new StringReader(gitHubArchiveEvent.payload.ToString()))
                                                    {
                                                        using (JsonTextReader jspayloadReader = new JsonTextReader(payLoadReader))
                                                        {
                                                            var pull = jSearial.Deserialize<PullRequestModel>(jspayloadReader);
                                                            sbHelper.SendMessage(string.Format("PullRequest;{0};{1};{2};{3};{4}", processDate, jobRunId, gitHubArchiveEvent.repo.id, pull.number,tokens[random.Next(0,10)]));

                                                        }
                                                    }
                                                    break;
                                                case "IssuesEvent":
                                                    using (StringReader payLoadReader = new StringReader(gitHubArchiveEvent.payload.ToString()))
                                                    {
                                                        using (JsonTextReader jspayloadReader = new JsonTextReader(payLoadReader))
                                                        {
                                                            var issue = jSearial.Deserialize<IssueModel>(jspayloadReader);
                                                            sbHelper.SendMessage(string.Format("Issue;{0};{1};{2};{3};{4}", processDate, jobRunId, gitHubArchiveEvent.repo.id, issue.issue.number, tokens[random.Next(0, 10)]));
                                                        }
                                                    }
                                                    break;
                                                case "PushEvent":
                                                    using (StringReader payLoadReader = new StringReader(gitHubArchiveEvent.payload.ToString()))
                                                    {
                                                        using (JsonTextReader jspayloadReader = new JsonTextReader(payLoadReader))
                                                        {
                                                            var push = jSearial.Deserialize<PushModel>(jspayloadReader);

                                                            foreach (var commit in push.commits)
                                                            {
                                                                sbHelper.SendMessage(string.Format("Commit;{0};{1};{2};{3};{4}", processDate, jobRunId, gitHubArchiveEvent.repo.id, commit.sha, tokens[random.Next(0, 10)]));
                                                            }
                                                        }
                                                    }
                                                    break;
                                                default: break;
                                            }
                                            sbHelper.SendMessage(string.Format("User;{0};{1};{2};{3};{4}", processDate, jobRunId, gitHubArchiveEvent.actor.login, string.Empty, tokens[random.Next(0, 10)]));
                                            sbHelper.SendMessage(string.Format("Repository;{0};{1};{2};{3};{4}", processDate, jobRunId, gitHubArchiveEvent.repo.id, string.Empty, tokens[random.Next(0, 10)]));
                                        }                           
                                    }

                                }
                            }
                        }
                    }
                }
            DatabaseHelper databaseHelper = new DatabaseHelper();
            var text = "UPDATE JobInfo SET CheckPointsCompletedAt=GetUTCDate()  WHERE JobRunId=" + jobRunId;
            databaseHelper.ExecuteNonQuery(text);

            //}
        }

    }
}
