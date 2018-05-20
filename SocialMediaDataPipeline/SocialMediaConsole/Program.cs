using Microsoft.ServiceBus.Messaging;
using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GithubHelper;
namespace SocialMediaConsole
{
    class Program
    {
        static string[] tokens = new string[] {
            "46c084306ff12438bd3443a610fac3359f703717",
            "87588201743c230c1e81b53edaa35b2a745da504",
            "2dc9699b640b5927759f3ccc1c39017a43def411",
            "7d4af7803e319bb3de4663b188084894621ebbce",
            "7753b555e0ed5eaf47e0e0a17eec4a399b984a06",
            "9e72c078785bfb7770e5de307852cacbd51e6b68",
            "9e0a1faa39e560aacfe1850e63454448cdd4303b",
            "c36ffa5023fe078e45c9a3cde9e3b3520e280618",
            "151655c170c9d3d433489a2d638da0ff45788b7b",
            "ec75320f40bca7434ed57a10119480df13dd3174"
        };
        
        static void Main(string[] args)
        {
            GithubCommonHelper githubCommonHelper;
            foreach (string token in tokens)
            {
                try
                {
                    githubCommonHelper = new GithubCommonHelper(token);
                    githubCommonHelper.CheckRateLimit();
                        } catch(Exception e)
                {
                    Console.WriteLine(string.Format("{0}:{1}", token, e.InnerException != null? e.InnerException.Message:e.Message));
                }
            }




            //int i = 0;
            //while (i<100)
            //{
            //    Random a = new Random();
            //    Console.WriteLine(a.Next(1, 11));
            //    i++;
            //}
            //string connStr = "Endpoint=sb://socialmediasb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Wi198MbCAvirOEjaT6MpZahWifihZFc6GMtm30icsb0=";
            //string queueName = "githubqueue";
            //QueueClient client = QueueClient.CreateFromConnectionString(connStr, queueName);
            //BrokeredMessage message = new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes
            //            ("test2")));
            //client.Send(message);
        }
    }
}
