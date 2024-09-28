using API_Producer_TechChallenge.Models.Regiao;
using Business_TechChallengePrimeiraFase.Regioes.Application.Interfaces;
using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
using Entities_TechChallengePrimeiraFase.Entities;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Regioes.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace API_Producer_TechChallenge.Controllers.Regioes
{
    [ApiController]
    [Route("[controller]")]
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoQueries _regiaoQueries;
        private readonly IRegiaoCommand _regiaoCommand;
        private readonly IValidacoesRegioes _validacoesRegioes;
        private readonly IRegiaoProducer _regiaoProducer;

        public RegiaoController( IRegiaoQueries regiaoQueries, IRegiaoCommand regiaoCommand, IValidacoesRegioes validacoesRegioes, IRegiaoProducer regiaoProducer)
        {
            _regiaoQueries = regiaoQueries;
            _regiaoCommand = regiaoCommand;
            _validacoesRegioes = validacoesRegioes;
            _regiaoProducer = regiaoProducer;
        }

        [HttpGet("GetRegioes")]
        public IActionResult GetRegioes()
        {
            var regioes = _regiaoCommand.GetRegioes();

            if (regioes != null)
            {
                return Ok(regioes);
            }
            else
            {
                return NoContent();
            }


        }

        [HttpGet("{id}")]
        public IActionResult GetRegiao(int id)
        {
            var regiao = _regiaoCommand.GetRegiao(id);

            if (regiao != null)
            {
                return Ok(regiao);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("InserirRegiao/siglaRegiao")]
        public IActionResult InserirRegiao(string siglaRegiao)
        {
            RegiaoEntity regiao = new RegiaoEntity() { Sigla = siglaRegiao.ToUpper() };

            if (_validacoesRegioes.ValidaRegiao(siglaRegiao))
            {
                var result = _regiaoProducer.InserirRegiao(JsonConvert.SerializeObject(regiao));

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return NoContent();
                }
            }
            return NoContent();

        }

        [HttpPut("AlterarRegiao/id")]
        public IActionResult AlterarRegiao(int id, [FromBody] RegioesModel regiao)
        {
            var regiaoEntity = _regiaoCommand.GetRegiao(id);

            if (regiaoEntity is not null)
            {
                regiaoEntity.Sigla = (String.IsNullOrEmpty(regiao.Sigla)) ? "" : regiao.Sigla;

                if (_validacoesRegioes.ValidaRegiao(regiaoEntity.Sigla))
                {
                    var result = _regiaoProducer.AlterarRegiao(JsonConvert.SerializeObject(regiaoEntity)); ;

                    if (result)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                return NoContent();
            }

            return NoContent();

        }

        [HttpDelete("DeletarRegiao/id")]
        public IActionResult DeletarRegiao(int id)
        {
            var regiaoEntity = _regiaoCommand.GetRegiao(id);

            if (regiaoEntity is not null)
            {
                var result = _regiaoProducer.ExcluirRegiao(id);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return NoContent();
                }
            }
            return NoContent();
        }
    }
}
