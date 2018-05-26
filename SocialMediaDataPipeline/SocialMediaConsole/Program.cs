using Microsoft.ServiceBus.Messaging;
using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GithubHelper;
using Newtonsoft.Json;
namespace SocialMediaConsole
{
    class Program
    {
        static string[] tokens = new string[] {
            ""
        };

        static void Main(string[] args)
        {
            GithubCommonHelper githubCommonHelper;
            //githubCommonHelper = new GithubCommonHelper("46c084306ff12438bd3443a610fac3359f703717");
            int remain = 0;
            foreach (string token in tokens)
            {
                githubCommonHelper = new GithubCommonHelper(token);
                remain = githubCommonHelper.CheckRateLimit("a");
                Console.WriteLine("token=" + token + "   remainrequest=" + remain);
            }
            //string user = githubCommonHelper.GetUsers("rashjz");

            //var o = JsonConvert.DeserializeObject<GithubUser>(user);

            //Console.WriteLine(JsonConvert.SerializeObject(o));





            int i = 0;
            while (i < 100)
            {
                Random a = new Random();
                Console.WriteLine(a.Next(1, 11));
                i++;
            }
            //string connStr = "Endpoint=sb://socialmediasb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Wi198MbCAvirOEjaT6MpZahWifihZFc6GMtm30icsb0=";
            //string queueName = "githubqueue";
            //QueueClient client = QueueClient.CreateFromConnectionString(connStr, queueName);
            //BrokeredMessage message = new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes
            //            ("test2")));
            //client.Send(message);
        }
    }
}
