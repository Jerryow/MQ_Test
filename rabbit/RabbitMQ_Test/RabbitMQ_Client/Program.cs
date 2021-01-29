using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ_Common.CommonModels;
using RabbitMQ_Common.Consumer;

namespace RabbitMQ_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQProvider rabbitMQProvider = new RabbitMQProvider("172.19.244.64", 5672, "articleone", "articleone");
            RabbitMQDirecterSubscriber rabbitMQSubscriber = new RabbitMQDirecterSubscriber(rabbitMQProvider, "articlequeue", "articleexchange");

            rabbitMQSubscriber.Subscribe("123", c =>
            {
                Console.WriteLine($"[A-收到消息]:{c}");
            });

            //Task.Factory.StartNew(() =>
            //{
            //    RabbitMQProvider rabbitMQProvider = new RabbitMQProvider("192.168.139.10", 5672, "articleone", "articleone");
            //    RabbitMQDirecterSubscriber rabbitMQSubscriber = new RabbitMQDirecterSubscriber(rabbitMQProvider, "articlequeue", "articleexchange");

            //    rabbitMQSubscriber.Subscribe("123", c =>
            //    {
            //        Console.WriteLine($"[A-收到消息]:{c}");
            //    });
            //});

            //Task.Factory.StartNew(() =>
            //{
            //    RabbitMQProvider rabbitMQProvider = new RabbitMQProvider("192.168.139.10", 5672, "articletwo", "articletwo");
            //    RabbitMQDirecterSubscriber rabbitMQSubscriber = new RabbitMQDirecterSubscriber(rabbitMQProvider, "articlequeue", "articleexchange");

            //    rabbitMQSubscriber.Subscribe("312", c =>
            //    {
            //        Console.WriteLine($"[b-收到消息]:{c}");
            //    });
            //});

            Console.ReadLine();
        }
    }

}
