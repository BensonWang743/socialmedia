using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = "Endpoint=sb://socialmediasb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Wi198MbCAvirOEjaT6MpZahWifihZFc6GMtm30icsb0=";
            string queueName = "githubqueue";
            QueueClient client = QueueClient.CreateFromConnectionString(connStr, queueName);
            BrokeredMessage message = new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes
                        ("test2")));
            client.Send(message);
        }
    }
}
