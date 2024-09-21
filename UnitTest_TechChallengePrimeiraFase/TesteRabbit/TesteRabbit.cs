using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Gateway.Producer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest_TechChallengePrimeiraFase.TesteRabbit
{

    public class TesteRabbit
    {
        [Fact]
        public void TesteRabbitConfig() 
        {
            var mensagemPublish = "TESTE";
            var mensagemConsume = "";

            ProducerTeste producerTeste = new ProducerTeste();

            producerTeste.TesteConexaoRabbitPublish(mensagemPublish);
            mensagemConsume = producerTeste.TesteConexaoRabbitConsume();

            Assert.Equal(mensagemConsume, mensagemPublish);
        }
    }
}
