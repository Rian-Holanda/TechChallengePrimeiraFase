using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Factory
{
    public class RabbitConfig
    {
        public ConnectionFactory Config()
        { 
            var factory = new ConnectionFactory() 
            { 
                HostName = "localhost",
                UserName = "guest",
                Port = 5672,
                Password = "guest"
                
            
            };

            return factory;
        }
    }
}
