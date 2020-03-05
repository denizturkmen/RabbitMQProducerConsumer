using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQExample
{
    public class RabbitMQService
    {
        //localde kurulu olduğu için ip ve remote verebilirsin 
        private string _hostName = "localhost";

        // IConnection üzerinden parametreleri set ettik
        // Güvenlik için USername ve password verebilirsin
        public IConnection GetRabbitMQConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(){
                HostName = _hostName
            //    Password = "Verebilirsin",
            //    UserName = "Verebilirsin"
            };
            return connectionFactory.CreateConnection();
        }
    }
}
