using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UnitTest_TechChallengePrimeiraFase.IntegrationTests.Regioes
{

    public class UserEndPointTest(ApplicationFactory<Program> factory) : IClassFixture<ApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        //[Fact]
        //public async void GetRegioes()
        //{
        //    var response = await _client.GetAsync("/Regiao/GetRegioes");
        //    if (response.IsSuccessStatusCode)
        //    {
              
        //    }
        //    else
        //    {
        //        Assert.Fail("Api call failed.");
        //    }
        //}
    }

}
