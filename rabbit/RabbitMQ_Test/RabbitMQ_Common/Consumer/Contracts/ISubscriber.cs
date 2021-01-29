using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ_Common.Consumer.Contracts
{
    public interface ISubscriber
    {
        /// <summary>
        /// 
        /// </summary>
        void Dispose();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channelName"></param>
        /// <returns></returns>
        void Subscribe(string channelName, Action<string> callback);
    }
}
