using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace GithubCrawler
{
    public class WorkerRole : RoleEntryPoint
    {
        // 队列的名称
        const string QueueName = "githubqueue";

        // QueueClient 是线程安全的。建议你进行缓存， 
        // 而不是针对每一个请求重新创建它
        QueueClient Client;
        ManualResetEvent CompletedEvent = new ManualResetEvent(false);
        string messageBody;

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
                    catch
                    {
                        // 在此处处理任何处理特定异常的消息
                        Trace.WriteLine("正在重新发送消息:" + messageBody);
                        BrokeredMessage newMessage = new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes
                        (messageBody)));
                        Client.Send(newMessage);
                        Thread.Sleep(1000*60);
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
            int a = 0;
            if (messageBody.Equals("test1"))
                Trace.WriteLine("正在开始处理消息:"+messageBody);
            else if (messageBody.Equals("test2"))
            {
                Trace.WriteLine("正在开始处理消息:" + messageBody);
                a = 1 / a;
            }
            
        }
    }
}