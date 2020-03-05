using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQExample
{
    class Program
    {
        private static string _queueName = "DENIZ";
        private static Publisher _publisher;
        private static Subscribee _subscribee;
        static void Main(string[] args)
        {
            _publisher = new Publisher(_queueName,"Sanane");
            _subscribee = new Subscribee(_queueName);
            Console.ReadLine();
        }
    }
}
