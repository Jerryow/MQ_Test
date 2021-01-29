using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ_Common.CommonModels;
using RabbitMQ_Common.Consumer.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ_Common.Consumer
{
    /// <summary>
    /// 消息订阅者/消费者。
    /// </summary>
    public class RabbitMQDirecterSubscriber : ISubscriber
    {
        private readonly RabbitMQProvider _provider;
        private IConnection _connection;
        private string _queueName;
        private string _exchangeName;
        public RabbitMQDirecterSubscriber(RabbitMQProvider provider, string queueName, string exchangeName)
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
            if (_channel != null)
            {
                _channel.Abort();
                if (_channel.IsOpen)
                    _channel.Close();

                _channel.Dispose();
            }

            if (_connection != null)
            {
                if (_connection.IsOpen)
                    _connection.Close();

                _connection.Dispose();
            }
        }

        /// <summary>
        /// 消费消息，并执行回调。
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="callback"></param>
        public void Subscribe(string aa, Action<string> callback)
        {
            //声明队列
            Channel.QueueDeclare(_queueName, true, false, false, null);
            //声明路由
            Channel.ExchangeDeclare(exchange:_exchangeName, type:"direct", durable: false, autoDelete: false, null);
            //绑定 建立关系
            Channel.QueueBind(_queueName, _exchangeName, "articleQueue_Exchange");

            //公平分发 同一时间只处理一个消息
            Channel.BasicQos(0, 1, true);
            var conSumer = new EventingBasicConsumer(Channel);
            conSumer.Received += (moede, e) =>
            {
                var body = e.Body;
                var msg = Encoding.UTF8.GetString(body.ToArray());
                callback(msg);
                //返回消息确认
                Channel.BasicAck(e.DeliveryTag, true);
            };
            //确认收到消息    进行消费
            Channel.BasicConsume(_queueName, false, conSumer);//false 手动应答；true：自动应答
        }
    }
}
