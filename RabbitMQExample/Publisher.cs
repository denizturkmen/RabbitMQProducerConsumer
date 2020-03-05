using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQExample
{
    //Bu bizim RabbitMq Prodecur karşılık geliyor
    public class Publisher
    {
        private RabbitMQService _rabbitMQService;

        public Publisher(string queueName, string message)
        {
            _rabbitMQService = new RabbitMQService();

            //connection açıldı
            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                //RabbitMQ üzerine yeni bir channel oluşturuyoruz
                //channel üzerinde queue oluşturarak istenilen mesaj bu channel üzerinden iletilir
                using (var channel = connection.CreateModel())
                {
                    //QueueDeclare durable:memory demi dursun yoksa disk yazılsınmı?
                    //QueueDeclare exclusive :Diğer conncetion ile kullanımına izin verilsin mi
                    //QueueDeclare autodelete :silme işlemi consumer okumuş ve işini bitirmiş ise silme ayarı
                    //QueueDeclare arguments : Belirlenen exchange ile ilgili parametredir
                    channel.QueueDeclare(queueName, false, false, false, null);

                    //BasicPublish Exchange : 4 tane var default directtir
                    //BasicPublish routinkey :girmiş olduğunuz keye göre yönlendirme yapar
                    //BasicPublish basiproperties :
                    //BasicPublish body : queue göndermek istediğimiz mesajı gönderdiğimiz biçim
                    channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(message));
                    Console.WriteLine("{0} queue'su üzerine, \"{1}\" mesajı yazıldı.", queueName, message);
                }
            }
        }
    }
}