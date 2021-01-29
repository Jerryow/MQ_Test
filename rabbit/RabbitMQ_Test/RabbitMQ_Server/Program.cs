using RabbitMQ.Client;
using RabbitMQ_Common.CommonModels;
using RabbitMQ_Common.Producer;
using System;
using System.Text;

namespace RabbitMQ_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQProvider rabbitMQProvider = new RabbitMQProvider("172.19.244.64", 5672, "articleadmin", "articleadmin");
            RabbitMQDirecterPublisher rabbitMQPublisher = new RabbitMQDirecterPublisher(rabbitMQProvider, "articlequeue", "articleexchange");

            var msg = Console.ReadLine();
            while (!string.IsNullOrEmpty(msg))
            {
                rabbitMQPublisher.Publish(msg);
                msg = Console.ReadLine();
                if (msg == "e")
                {
                    msg = "";
                    rabbitMQPublisher.Dispose();
                }
            }

            Console.ReadLine();
        }
    }

    
}
