using API_TechChallengePrimeiraFase.Models.Regiao;
using Business_TechChallengePrimeiraFase.Regioes.Application.Interfaces;
using DataAccess_TechChallengePrimeiraFase.Regioes.Command;
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
    public class RegioesCodigosAreasController : ControllerBase
    {
        private readonly IRegiaoQueries _regiaoQueries;
        private readonly IRegiaoCodigoAreaCommand _regiaoCodigoAreaCommand;
        private readonly IRegiaoCodigoAreaQueries _regiaoCodigoAreaQueries;
        private readonly IValidacoesRegioes _validacoesRegioes;

        public RegioesCodigosAreasController(IRegiaoQueries regiaoQueries, IRegiaoCodigoAreaCommand regiaoCodigoAreaCommand, IRegiaoCodigoAreaQueries regiaoCodigoAreaQueries, IValidacoesRegioes validacoesRegioes)
        {
            _regiaoQueries = regiaoQueries;
            _regiaoCodigoAreaCommand = regiaoCodigoAreaCommand;
            _regiaoCodigoAreaQueries = regiaoCodigoAreaQueries;
            _validacoesRegioes = validacoesRegioes;
        }

        [HttpGet("RegioesCodigosAreas")]
        public IActionResult GetCodigosAreasRegioes()
        {
            var regioesCodigosAreas = _regiaoCodigoAreaCommand.GetRegioesCodigosAreas();

            if (regioesCodigosAreas != null)
            {
                return Ok(regioesCodigosAreas);
            }
            else
            {
                return NoContent();
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetRegiaoCodigoArea(int id)
        {
            var regiaoCodigoArea = _regiaoCodigoAreaCommand.GetRegiaoCodigoArea(id);

            if (regiaoCodigoArea != null)
            {
                return Ok(regiaoCodigoArea);
            }
            else
            {
                return NoContent();
            }
        }


        [HttpPost("InserirRegiaoCodigoArea")]
        public IActionResult InserirRegiaoCodigoArea([FromBody] RegiaoCodigoAreaModel regiaoCodigoAreaModel)
        {
            var regiao = _regiaoQueries.GetRegiaoExistente(regiaoCodigoAreaModel.siglaRegiao);

            if (regiao is not null)
            {
                RegioesCodigosAreasEntity regiaoCodigoArea = new RegioesCodigosAreasEntity()
                {
                    Regiao = regiao,
                    IdRegiao = regiao.Id,
                    DDD = regiaoCodigoAreaModel.DDD,
                };


                var result = _regiaoCodigoAreaCommand.InserirRegiaoCodigoArea(regiaoCodigoArea);

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

        [HttpPut("AlterarRegiaoCodigoArea/id")]
        public IActionResult AlterarRegiaoCodigoArea(int id, [FromBody] RegiaoCodigoAreaModel regiaoCodigoAreaModel)
        {
            var regiaoCodigoArea = _regiaoCodigoAreaCommand.GetRegiaoCodigoArea(id);
            var regiao = _regiaoQueries.GetRegiaoExistente(regiaoCodigoAreaModel.siglaRegiao);

            if (regiaoCodigoArea is not null && regiao is not null)
            {
                regiaoCodigoArea.IdRegiao = regiao.Id;
                regiaoCodigoArea.DDD = regiaoCodigoAreaModel.DDD;

                var result = _regiaoCodigoAreaCommand.AlterarRegiaoCodigoArea(regiaoCodigoArea, id);

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

        [HttpDelete("DeletarRegiaoCodigoArea/id")]
        public IActionResult DeletarRegiaoCodigoArea(int id)
        {
            var regiaoCodigoArea = _regiaoCodigoAreaCommand.GetRegiaoCodigoArea(id);

            if (regiaoCodigoArea is not null)
            {
                var result = _regiaoCodigoAreaCommand.ExcluirRegiaoCodigoArea(regiaoCodigoArea.Id);

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
