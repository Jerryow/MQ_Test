using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ_Common.Producer.Contracts
{
    public interface IPublisher
    {
        /// <summary>
        /// 释放资源。
        /// </summary>
        void Dispose();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        void Publish<T>(T message) where T : class;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="channelName"></param>
        void Publish(string message, string channelName);
    }
}
