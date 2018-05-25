using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;

namespace SocialMediaAF
{
    public static class GithubTrigger
    {
        [FunctionName("GithubTrigger")]
        public static void Run([TimerTrigger("0 */10 * * * *")]TimerInfo myTimer, TraceWriter log, ExecutionContext context, [ServiceBus("githubqueue", Connection = "socialmediasb")] out string msg)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            string dbStr = config.GetConnectionString("socialmedia");
            string sbStr = config["socialmediasb"];

            //Get message count in queue
            var nsMGR = NamespaceManager.CreateFromConnectionString(sbStr);
            long msgCount = nsMGR.GetQueue("githubqueue").MessageCount;
             
            DateTime maxUpdate = DateTime.MaxValue;
            string status = string.Empty;
            bool isNotified = false;
            int jobRunId = 0;
            DateTime checkPoint = DateTime.MaxValue;
            using (SqlConnection conn = new SqlConnection(dbStr))
            {
                try
                {
                    conn.Open();
                    var text = "SELECT TOP 1 MaxUpdate,Status,IsNotified,JobRunId,CheckPointsCompletedAt FROM dbo.JobInfo "
                            + "WHERE Platform='Github' ORDER BY JobRunId DESC";

                    using (SqlCommand cmd = new SqlCommand(text, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                maxUpdate = reader.GetDateTime(0);
                                status = reader.GetString(1);
                                isNotified = reader.GetBoolean(2);
                                jobRunId = reader.GetInt32(3);
                                checkPoint = reader.IsDBNull(4)? DateTime.MaxValue:reader.GetDateTime(4);
                            }
                        }
                    }
                    if (status == "Completed" && msgCount == 0)
                    {

                        maxUpdate = maxUpdate.AddHours(1);
                        //update db status
                        text = "INSERT INTO JobInfo " +
                            "SELECT 'GitHub','" + maxUpdate + "','" + DateTime.UtcNow + "',null,null,'InProgress',0,null";
                        using (SqlCommand cmd = new SqlCommand(text, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        //generate new message 
                        msg = string.Format("{0};{1};{2}", "Start", maxUpdate, jobRunId + 1);
                    }
                    else if (status == "InProgress" && msgCount == 0 && isNotified == false && checkPoint!=DateTime.MaxValue)
                    {
                        text = "UPDATE JobInfo SET Status='Completed', IsNotified=1,JobEndAt=GetUtcDate() WHERE JobRunId=" + jobRunId;
                        using (SqlCommand cmd = new SqlCommand(text, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        //generate new message 
                        msg = string.Format("{0};{1}", "NotifyComplete", string.Empty);
                    }
                    else if (status == "Failed" && msgCount != 0 && isNotified == false)
                    {
                        text = "UPDATE JobInfo SET Status='InProgress' WHERE JobRunId=" + jobRunId;
                        using (SqlCommand cmd = new SqlCommand(text, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        msg = string.Format("{0};{1}", "ReactiveDeadMessage", string.Empty);
                    }
                    else
                    {
                        msg = "Inprogress";
                    }
                }
                catch (Exception e)
                { 
                    log.Info($"Throw Exception: {e.Message}");
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
