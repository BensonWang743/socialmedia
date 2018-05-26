using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaCommonHelper
{
    public class ServiceBusHelper
    {
        static string[] tokens = new string[] {
            ""
        };


        QueueClient queueClient;
        public ServiceBusHelper(string queueName, string connectionString)
        {
            queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);
        }

        public void SendMessage(string message)
        {
            BrokeredMessage msg = new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes(message)));
            queueClient.Send(msg);
        }
        public void SendBatchMessage(HashSet<string> messageList)
        {
            int maxBatchSizeInBytes = 240000;
            long currnetBatchSize =0;
            int tokenIndex = 0;
            List<BrokeredMessage> msgs = new List<BrokeredMessage>();
            foreach (string m in messageList)
            {
                string messageTemp =string.Empty;
                if (tokenIndex >= 25)
                    tokenIndex = 0;
                messageTemp = string.Format("{0}{1}", m, tokens[tokenIndex]);
                BrokeredMessage message=new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes(messageTemp)));
                if (currnetBatchSize + message.Size < maxBatchSizeInBytes)
                {
                    msgs.Add(message);
                    currnetBatchSize += message.Size+54;
                }
                else
                {
                    queueClient.SendBatch(msgs);
                    msgs.Clear();
                    msgs.Add(message);
                    currnetBatchSize = message.Size+54;
                }
                tokenIndex = tokenIndex + 1;
            }
            if(msgs.Any())
                queueClient.SendBatch(msgs);
        }
    }
}
