using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQExample
{
    //RabbitMQ Consumer karşılık 
    public class Subscribee
    {
        private RabbitMQService _rabbitMqService;

        public Subscribee(string queueName)
        {
            _rabbitMqService = new RabbitMQService();

            using (var connection = _rabbitMqService.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // EventingBasicConsumer channel geldiğinde received sayesinde sürekli listening olcak
                    // Queue mesajları sırasıyla almaktadır
                    var consumer = new EventingBasicConsumer(channel);
                    // Received event'i sürekli listen modunda olacaktır.
                    //
                    consumer.Received += (model, ea) =>
                    {
                        //Producerdan byte olarak gönderdik ve consumer string geleceğibi bildiğimiz için
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("{0} isimli queue üzerinden gelen mesaj: \"{1}\"", queueName, message);
                    };
                    // BasicConsume : hangi queuw mesajları alınacak ise
                    // BasicConsume :noack true ise consumer mesajı aldığpı zaman otomatik siler
                    channel.BasicConsume(queueName, false, consumer);
                    Console.ReadLine();
                }
                    
            }
        }
    }
}
