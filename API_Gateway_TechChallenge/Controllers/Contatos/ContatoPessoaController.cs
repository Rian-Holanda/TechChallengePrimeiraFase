using Business_TechChallengePrimeiraFase.Contatos.Application.Interfaces;
using DataAccess_TechChallengePrimeiraFase.Contatos.Interfaces;
using Entities_TechChallengePrimeiraFase.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using DataAccess_TechChallengePrimeiraFase.Contatos.Interface;
using API_Gateway_TechChallenge.Models.Pessoa;
using Business_TechChallengePrimeiraFase.Contatos.Domain;
using DataAccess_TechChallengePrimeiraFase.Contatos.Command;
using DataAccess_TechChallengePrimeiraFase.Regioes.Command;
using DataAccess_TechChallengePrimeiraFase.Regioes.Interface;
using static Business_TechChallengePrimeiraFase.Contatos.Domain.ValidaContatoPessoa;
using Entities_TechChallengePrimeiraFase.Enuns;

namespace API_Gateway_TechChallenge.Controllers.Contatos
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoPessoaController : ControllerBase
    {
        private readonly IContatosPessoasQueries _contatosPessoasQueries;
        private readonly IContatosPessoasCommand _contatosPessoasCommand;
        private readonly IPessoasCommand _pessoasCommand;
        private readonly IRegiaoQueries _regiaoQueries;

        private ContatoPessoaDomain contatoPessoaDomain = new ContatoPessoaDomain();

        public ContatoPessoaController(IContatosPessoasQueries contatosPessoasQueries, IContatosPessoasCommand contatosPessoasCommand, IPessoasCommand pessoasCommand, IRegiaoQueries regiaoQueries) 
        {
            _contatosPessoasQueries = contatosPessoasQueries;
            _contatosPessoasCommand = contatosPessoasCommand;
            _pessoasCommand = pessoasCommand;
            _regiaoQueries = regiaoQueries; 
        }

        [HttpGet("GetContatosPessoas")]
        public IActionResult GetContatosPessoas()
        {
            var contatosPessoas = _contatosPessoasCommand.GetContatosPessoas();

            if (contatosPessoas != null)
            {
                return Ok(contatosPessoas);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetContatoPessoa(int id)
        {
         
            var contatoPessoa = _contatosPessoasCommand.GetContatoPessoa(id);

            if (contatoPessoa != null)
            {
                
                
                return Ok(contatoPessoa);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("InserirContatoPessoa")]
        public async Task<IActionResult> InserirContatoPessoa([FromBody] ContatoPessoaModel contatoPessoaModel)
        {
            var pessoa = _pessoasCommand.GetPessoa(contatoPessoaModel.IdPessoa);
            var regiao = _regiaoQueries.GetRegiaoExistente(contatoPessoaModel.SiglaRegiao);

            if (regiao is not null && pessoa is not null) 
            {
                ContatoPessoaEntity ContatoPessoaEntity = new ContatoPessoaEntity()
                {
                    Pessoa = pessoa,
                    Regiao = regiao,
                    Numero = contatoPessoaModel.Numero,
                    IdPessoa = pessoa.Id,
                    IdRegiao = regiao.Id,
                    TipoContatoPessoa = (contatoPessoaModel.ContatoCelular)? (int) TipoContatoEnum.Celular: (int)TipoContatoEnum.Fixo
                };

                var resultValidacao = (contatoPessoaModel.ContatoCelular) ?
                                       contatoPessoaDomain.ValidaTipoContato(new ValidaContatoPessoaCelular(), ContatoPessoaEntity) :
                                       contatoPessoaDomain.ValidaTipoContato(new ValidaContatoPessoaFixo(), ContatoPessoaEntity);

                var resultValidacaoContato = contatoPessoaDomain.Validate(ContatoPessoaEntity);

                if (resultValidacao && resultValidacaoContato.IsValid)
                {
                    var result = await _contatosPessoasCommand.InserirContatoPessoa(ContatoPessoaEntity);

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

            return NoContent();
        }

        [HttpPut("AlterarContatoPessoa/id")]
        public async Task<IActionResult> AlterarContatoPessoa(int id, [FromBody] ContatoPessoaModel contatoPessoaModel)
        {
            var pessoa = _pessoasCommand.GetPessoa(contatoPessoaModel.IdPessoa);
            var regiao = _regiaoQueries.GetRegiaoExistente(contatoPessoaModel.SiglaRegiao);

            if (regiao is not null && pessoa is not null)
            {
                ContatoPessoaEntity ContatoPessoaEntity = new ContatoPessoaEntity()
                {
                    Pessoa = pessoa,
                    Regiao = regiao,
                    Numero = contatoPessoaModel.Numero,
                    IdPessoa = pessoa.Id,
                    IdRegiao = regiao.Id,
                    TipoContatoPessoa = (contatoPessoaModel.ContatoCelular) ? (int)TipoContatoEnum.Celular : (int)TipoContatoEnum.Fixo
                };

                var resultValidacao = (contatoPessoaModel.ContatoCelular) ?
                                    contatoPessoaDomain.ValidaTipoContato(new ValidaContatoPessoaCelular(), ContatoPessoaEntity) :
                                    contatoPessoaDomain.ValidaTipoContato(new ValidaContatoPessoaFixo(), ContatoPessoaEntity);

                var resultValidacaoContato = contatoPessoaDomain.Validate(ContatoPessoaEntity);

                if (resultValidacao && resultValidacaoContato.IsValid)
                {
                    var result = await _contatosPessoasCommand.AlterarContatoPessoa(ContatoPessoaEntity, id);

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

        [HttpDelete("DeletarContatoPessoa/id")]
        public async Task<IActionResult> DeletarContatoPessoa(int id)
        {
            var contatoPessoaEntity = _contatosPessoasCommand.GetContatoPessoa(id);

            if (contatoPessoaEntity is not null)
            {
                var result = await _contatosPessoasCommand.ExcluirContatoPessoa(contatoPessoaEntity.Id);

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
