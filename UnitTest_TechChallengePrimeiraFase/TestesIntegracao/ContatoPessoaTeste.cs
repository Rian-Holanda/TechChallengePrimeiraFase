using API_TechChallengePrimeiraFase.Teste;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest_TechChallengePrimeiraFase.TestesIntegracao
{
    public class ContatoPessoaTeste : IClassFixture<WebApplicationFactory<IApiMarker>>
    {
        private readonly HttpClient _httpClient;

        public ContatoPessoaTeste(WebApplicationFactory<IApiMarker> appFactory)
        {
            _httpClient = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetPessoas()
        {
            var response = await _httpClient.GetAsync($"/Pessoa/GetPessoas");

            var teste = response;

            Assert.NotNull(response);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        }
    }
}
