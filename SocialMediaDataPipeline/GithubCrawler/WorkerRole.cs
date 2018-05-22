using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using GithubHelper;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using SocialMediaCommonHelper;

namespace GithubCrawler
{
    public class WorkerRole : RoleEntryPoint
    {
        // 队列的名称
        const string QueueName = "githubqueue";
        const string dbConStr = "Server=tcp:socialmediaserver.database.windows.net,1433;Initial Catalog=socialmediadb;Persist Security Info=False;User ID=socialmedia;Password=S0cialmedia!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=300;";

        // QueueClient 是线程安全的。建议你进行缓存， 
        // 而不是针对每一个请求重新创建它
        QueueClient Client;
        ManualResetEvent CompletedEvent = new ManualResetEvent(false);
        string messageBody;
        GithubArchieveFileProcessor gitProcessor = new GithubArchieveFileProcessor();
        DatabaseHelper databaseHelper = new DatabaseHelper(dbConStr);
        

        public override void Run()
        {
           Trace.WriteLine("正在开始处理消息");
            var options = new OnMessageOptions();

            // options.AutoComplete = false;
            options.MaxConcurrentCalls = 1;
            

            // 启动消息泵，并且将为每个已收到的消息调用回调，在客户端上调用关闭将停止该泵。
            Client.OnMessage((receivedMessage) =>
                {
                    try
                    {
                        // 处理消息
                        messageBody = new StreamReader(receivedMessage.GetBody<Stream>()).ReadToEnd();
                        RunMain(messageBody);
                    }
                    catch(Exception e)
                    {
                        // 在此处处理任何处理特定异常的消息
                        Trace.WriteLine("RE-SEND MESSAGE:" + messageBody);
                        BrokeredMessage newMessage = new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes
                        (messageBody)));
                        Client.Send(newMessage);
                        string errorMsg = string.Empty;
                        if (e.InnerException != null)
                            errorMsg = e.GetBaseException().Message;
                        else
                            errorMsg = e.Message;
                        var text = "UPDATE JobInfo SET ErrorMessage='"+ errorMsg + "' WHERE JobRunId=" + messageBody.Split(';')[2];
                        databaseHelper.ExecuteNonQuery(text);
                        text = "INSERT INTO LogTrace VALUES('Error','"+messageBody+"','"+ errorMsg + "',GetUtcDate())";
                        databaseHelper.ExecuteNonQuery(text);
                    }
                },options);

            CompletedEvent.WaitOne();
        }

        public override bool OnStart()
        {
            // 设置最大并发连接数 
            ServicePointManager.DefaultConnectionLimit = 12;

            // 如果队列不存在，则创建队列
            string connectionString = "Endpoint=sb://socialmediasb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Wi198MbCAvirOEjaT6MpZahWifihZFc6GMtm30icsb0=";

            // 初始化与 Service Bus 队列的连接
            Client = QueueClient.CreateFromConnectionString(connectionString, QueueName,ReceiveMode.ReceiveAndDelete);
            return base.OnStart();
        }

        public override void OnStop()
        {
            // 关闭与 Service Bus 队列的连接
            Client.Close();
            CompletedEvent.Set();
            base.OnStop();
        }
        public void RunMain(string messageBody)
        {
            if (messageBody.StartsWith("Start"))
            {
                gitProcessor.GitHubArchiveParser(Convert.ToDateTime(messageBody.Split(';')[1]), Convert.ToInt32(messageBody.Split(';')[2]));
            }
            else if (messageBody.StartsWith("PullRequest"))
            {
                PullRequestProcessor pulls = new PullRequestProcessor(messageBody);
                pulls.GetContent();
            } else if (messageBody.StartsWith("Commit"))
            {
                CommitProcessor commits = new CommitProcessor(messageBody);
                commits.GetContent();
            }
            else if (messageBody.StartsWith("Issue"))
            {
                IssueProcessor issues = new IssueProcessor(messageBody);
                issues.GetContent();
            }
            else if (messageBody.StartsWith("User"))
            {
                UserProcessor users = new UserProcessor(messageBody);
                users.GetContent();
            }
            else if (messageBody.StartsWith("Repository"))
            {
                RepositoryProcessor repositorys = new RepositoryProcessor(messageBody);
                repositorys.GetContent();
            }

        }
    }
}