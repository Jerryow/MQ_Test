using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ_Common.CommonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ_Common.Producer
{
    //生产/消费模式
    public class RabbitMQDirecterPublisher
    {
        private readonly RabbitMQProvider _provider;
        private IConnection _connection;
        private string _queueName;
        private string _exchangeName;
        public RabbitMQDirecterPublisher(RabbitMQProvider provider, string queueName, string exchangeName)
        {
            _provider = provider;
            _connection = _provider.ConnectionFactory.CreateConnection();
            _queueName = queueName;
            _exchangeName = exchangeName;
        }

        public IConnection Connection
        {
            get
            {
                if (_connection != null)
                    return _connection;
                return _connection = _provider.ConnectionFactory.CreateConnection();
            }
        }

        private IModel _channel;
        public IModel Channel
        {
            get
            {
                if (_channel != null)
                    return _channel;
                else
                    return _channel = _connection.CreateModel();
            }
        }


        public void Dispose()
        {
            if (Channel != null)
            {
                if (Channel.IsOpen)
                    Channel.Close();
                Channel.Abort();
                Channel.Dispose();
            }

            if (Connection != null)
            {
                if (Connection.IsOpen)
                    Connection.Close();
            }
        }

        public void Publish<T>(T message) where T : class
        {
            var channelName = typeof(T).Name;
            Channel.QueueDeclare(_queueName, true, false, false, null);
            Channel.ExchangeDeclare(exchange: _exchangeName, type: "direct", durable: false, autoDelete: false, null);
            //绑定 建立关系
            Channel.QueueBind(_queueName, _exchangeName, "articleQueue_Exchange");

            //内容的基本属性
            var properties = Channel.CreateBasicProperties();
            //设置消息内容持久化
            properties.Persistent = true;

            var msgContent = JsonConvert.SerializeObject(message);
            var msgByte = Encoding.UTF8.GetBytes(msgContent);
            Channel.BasicPublish(exchange: channelName, routingKey: "articleQueue_Exchange", mandatory: false, basicProperties: properties, body: msgByte);
        }


        public void Publish(string message)
        {
            Channel.QueueDeclare(_queueName, true, false, false, null);
            Channel.ExchangeDeclare(exchange: _exchangeName, type: "direct", durable: false, autoDelete: false, null);
            //绑定 建立关系
            Channel.QueueBind(_queueName, _exchangeName, "articleQueue_Exchange");

            var msgByte = Encoding.UTF8.GetBytes(message);
            Channel.BasicPublish
            (
                exchange: _exchangeName,
                routingKey: "articleQueue_Exchange",
                mandatory: false,
                basicProperties: null,
                body: msgByte
            );
        }
    }
}
