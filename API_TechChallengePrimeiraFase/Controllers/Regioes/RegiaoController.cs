using API_TechChallengePrimeiraFase.Models.Regiao;
using Business_TechChallengePrimeiraFase.Regioes.Application.Interfaces;
using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
using Entities_TechChallengePrimeiraFase.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace API_TechChallengePrimeiraFase.Controllers.Regioes
{
    [ApiController]
    [Route("[controller]")]
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoQueries _regiaoQueries;
        private readonly IRegiaoCommand _regiaoCommand;
        private readonly IValidacoesRegioes _validacoesRegioes;

        public RegiaoController( IRegiaoQueries regiaoQueries, IRegiaoCommand regiaoCommand, IValidacoesRegioes validacoesRegioes)
        {
            
            _regiaoQueries = regiaoQueries;
            _regiaoCommand = regiaoCommand;
            _validacoesRegioes = validacoesRegioes;
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
            RegioesEntity regiao = new RegioesEntity() { Sigla = siglaRegiao.ToUpper() };

            if (_validacoesRegioes.ValidaRegiao(siglaRegiao))
            {
                var result = _regiaoCommand.InserirRegiao(regiao);

                if (result > 0)
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
                    var result = _regiaoCommand.AlterarRegiao(regiaoEntity, id);

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
                var result = _regiaoCommand.ExcluirRegiao(regiaoEntity.Id);

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
