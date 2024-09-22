using API_Producer_TechChallenge.Models.Regiao;
using Business_TechChallengePrimeiraFase.Regioes.Application.Interfaces;
using DataAccess_TechChallengePrimeiraFase.Regioes.Command;
using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
using Entities_TechChallengePrimeiraFase.Entities;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Regioes.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace API_Producer_TechChallenge.Controllers.Regioes
{
    [ApiController]
    [Route("[controller]")]
    public class RegioesCodigosAreasController : ControllerBase
    {
        private readonly IRegiaoQueries _regiaoQueries;
        private readonly IRegiaoCodigoAreaCommand _regiaoCodigoAreaCommand;
        private readonly IRegiaoCodigoAreaQueries _regiaoCodigoAreaQueries;
        private readonly IValidacoesRegioes _validacoesRegioes;
        private readonly IRegiaoCodigoAreaProducer _regiaoCodigoAreaProducer;

        public RegioesCodigosAreasController(IRegiaoQueries regiaoQueries, IRegiaoCodigoAreaCommand regiaoCodigoAreaCommand, IRegiaoCodigoAreaQueries regiaoCodigoAreaQueries, IValidacoesRegioes validacoesRegioes, IRegiaoCodigoAreaProducer regiaoCodigoAreaProducer)
        {
            _regiaoQueries = regiaoQueries;
            _regiaoCodigoAreaCommand = regiaoCodigoAreaCommand;
            _regiaoCodigoAreaQueries = regiaoCodigoAreaQueries;
            _validacoesRegioes = validacoesRegioes;
            _regiaoCodigoAreaProducer = regiaoCodigoAreaProducer;
        }

        [HttpGet("RegioesCodigosAreas")]
        public IActionResult GetCodigosAreasRegioes()
        {
            var regioesCodigosAreas = _regiaoCodigoAreaCommand.GetRegioesCodigosAreas();
            List<RegiaoCodigoAreaModel> regioesCodigosAreasModel = new List<RegiaoCodigoAreaModel>();

            if (regioesCodigosAreas != null)
            {
                regioesCodigosAreasModel.AddRange(regioesCodigosAreas.Select(r => new RegiaoCodigoAreaModel()
                {
                    DDD = r.DDD,
                    siglaRegiao = r.Regiao.Sigla
                }));

                return Ok(regioesCodigosAreasModel);
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

        [HttpGet("GetSiglaCodigoArea/ddd")]
        public IActionResult GetSiglaCodigoArea(int ddd)
        {
            var regiaoCodigoArea = _regiaoCodigoAreaQueries.GetSiglaCodigoArea(ddd);

            if (regiaoCodigoArea != null)
            {
                return Ok(regiaoCodigoArea);
            }
            else
            {
                return NoContent();
            }
        }


        [HttpGet("GetDDDs/siglaRegiao")]
        public IActionResult GetSiglaCodigoArea(string siglaRegiao)
        {
            var DDDs = _regiaoCodigoAreaQueries.GetDDDs(siglaRegiao);

            if (DDDs != null)
            {
                return Ok(DDDs);
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

                var result = _regiaoCodigoAreaProducer.InserirRegiaoCodigoArea(JsonConvert.SerializeObject(regiaoCodigoArea));

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

        [HttpPut("AlterarRegiaoCodigoArea/id")]
        public IActionResult AlterarRegiaoCodigoArea(int id, [FromBody] RegiaoCodigoAreaModel regiaoCodigoAreaModel)
        {
            var regiaoCodigoArea = _regiaoCodigoAreaCommand.GetRegiaoCodigoArea(id);
            var regiao = _regiaoQueries.GetRegiaoExistente(regiaoCodigoAreaModel.siglaRegiao);

            if (regiaoCodigoArea is not null && regiao is not null)
            {
                regiaoCodigoArea.IdRegiao = regiao.Id;
                regiaoCodigoArea.DDD = regiaoCodigoAreaModel.DDD;

                var result = _regiaoCodigoAreaProducer.AlterarRegiaoCodigoArea(JsonConvert.SerializeObject(regiaoCodigoArea)); ;

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
                var result = _regiaoCodigoAreaProducer.ExcluirRegiaoCodigoArea(id);

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
