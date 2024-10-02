using API_TechChallengePrimeiraFase.Models.Regiao;
using API_Producer_TechChallenge.Teste;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest_TechChallengePrimeiraFase.TestesIntegracao
{
    public class RegiaoTeste :
   IClassFixture<WebApplicationFactory<IApiMarker>>
    {
        private readonly HttpClient _httpClient;

        public RegiaoTeste(WebApplicationFactory<IApiMarker> appFactory)
        {
            _httpClient = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetRegioes()
        {
            var response = await _httpClient.GetAsync($"/Regiao/GetRegioes");

            var teste = response;

            Assert.NotNull(response);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task GetSiglaCodigoArea()
        {
            var response = await _httpClient.GetAsync($"/RegioesCodigosAreas/GetSiglaCodigoArea/ddd?ddd=11");

            var teste = response;

            Assert.NotNull(response);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            if (response != null)
            {
                string codigoArea = response.Content.ReadAsStringAsync().Result;

                Assert.Equal("SP", codigoArea);
            }
           

        }

    [Fact]
    public async Task GetDDDs()
    {
        var response = await _httpClient.GetAsync($"/RegioesCodigosAreas/GetDDDs/siglaRegiao?siglaRegiao=SP");

        var teste = response;

        var ddds = await response.Content.ReadFromJsonAsync<List<int>>();

        if (ddds != null)
        {
            Assert.NotNull(response);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            var ddd = ddds.Where(a => a == 11).Count();

            Assert.Equal(1, ddd);
        }


    }
}
}
