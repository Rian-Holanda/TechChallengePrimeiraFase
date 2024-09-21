using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Gateway;
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

            ProducerPessoa producerPessoa = new ProducerPessoa();

            producerPessoa.TesteConexaoRabbitPublish(mensagemPublish);

            mensagemConsume = producerPessoa.TesteConexaoRabbitConsume();

            Assert.Equal(mensagemConsume, mensagemPublish);
        }
    }
}
