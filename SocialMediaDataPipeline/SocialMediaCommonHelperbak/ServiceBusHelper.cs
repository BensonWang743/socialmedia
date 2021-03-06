﻿using Microsoft.ServiceBus.Messaging;
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
        QueueClient queueClient;
        public ServiceBusHelper(string queueName,string connectionString)
        {
            queueClient= QueueClient.CreateFromConnectionString(connectionString, queueName);
        }

        public void SendMessage(string message)
        {
            BrokeredMessage msg = new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes(message)));
            queueClient.Send(msg);
        }
    }
}
