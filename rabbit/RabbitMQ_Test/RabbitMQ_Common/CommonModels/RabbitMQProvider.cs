using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ_Common.CommonModels
{
    public class RabbitMQProvider
    {
        private readonly string _ipAddress;
        private readonly int? _port;
        private readonly string _username;
        private readonly string _password;

        public RabbitMQProvider(string ipAddress, int? port, string username, string password)
        {
            _ipAddress = ipAddress ?? throw new ArgumentException("IP地址不能为空！");
            _port = port ?? throw new ArgumentException("端口不能为空");
            _username = username ?? throw new ArgumentException("用户名不能为空");
            _password = password ?? throw new ArgumentException("密码不能为空");

            ConnectionFactory = new ConnectionFactory//创建连接工厂对象
            {
                HostName = _ipAddress,//IP地址
                Port = (int)_port,//端口号
                UserName = _username,//用户账号
                Password = _password,//用户密码
                VirtualHost = "fxeye_vhost"
            };
        }

        public IConnectionFactory ConnectionFactory { get; }
    }
}
