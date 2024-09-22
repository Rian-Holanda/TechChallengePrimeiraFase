using Business_TechChallengePrimeiraFase.Contatos.Application.Interfaces;
using Business_TechChallengePrimeiraFase.Contatos.Domain;
using DataAccess_TechChallengePrimeiraFase.Contatos.Interface;
using DataAccess_TechChallengePrimeiraFase.Contatos.Interfaces;
using Entities_TechChallengePrimeiraFase.Entities;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Gateway;
using Microsoft.AspNetCore.Mvc;

namespace Servico_Pessoas_TechChellenge.Controllers.Contatos
{
    public class PessoaController : ControllerBase
    {

        private readonly IPessoasCommand _pessoasCommand;
        private readonly IValidaEmailPessoa _validaEmailPessoa;

        private ValidaPessoa validaPessoa = new ValidaPessoa();

        public PessoaController( IPessoasCommand pessoasCommand, IValidaEmailPessoa validaEmailPessoa)
        {

            _pessoasCommand = pessoasCommand;
            _validaEmailPessoa = validaEmailPessoa;
        }


        [HttpGet("GetPessoasConsumer")]
        public IActionResult GetPessoasProducer() 
        {
            var pessoas = _pessoasCommand.GetPessoas();

            return Ok(pessoas);

        }


        [HttpGet("{id}")]
        public IActionResult GetPessoa(int id)
        {
            var pessoa = _pessoasCommand.GetPessoa(id);

            if (pessoa != null)
            {
                Pessoa pessoasModel = new Pessoa()
                {
                    Id = pessoa.Id,
                    Nome = pessoa.Nome,
                    Email = pessoa.Email
                };

                return Ok(pessoasModel);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("InserirPessoa")]
        public IActionResult InserirPessoa([FromBody] Pessoa pessoasModel)
        {

            PessoasEntity pessoa = new PessoasEntity()
            {
                Nome = pessoasModel.Nome,
                Email = pessoasModel.Email
            };

            if (_validaEmailPessoa.ValidaEmail(pessoa.Email))
            {
                var resultValidacao = validaPessoa.Validate(pessoa);

                var result = _pessoasCommand.InserirPessoa(pessoa);

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

        [HttpPut("AlterarPessoa/id")]
        public IActionResult AlterarPessoa(int id, [FromBody] Pessoa pessoasModel)
        {
            var pessoaEntity = _pessoasCommand.GetPessoa(id);

            if (pessoaEntity is not null)
            {
                pessoaEntity.Nome = pessoasModel.Nome;
                pessoaEntity.Email = pessoasModel.Email;

                if (_validaEmailPessoa.ValidaEmail(pessoaEntity.Email))
                {
                    var resultValidacao = validaPessoa.Validate(pessoaEntity);

                    var result = _pessoasCommand.AlterarPessoa(pessoaEntity, id);

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

        [HttpDelete("DeletarPessoa/id")]
        public IActionResult DeletarPessoa(int id)
        {
            var pessoaEntity = _pessoasCommand.GetPessoa(id);

            if (pessoaEntity is not null)
            {
                var result = _pessoasCommand.ExcluirPessoa(pessoaEntity.Id);

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
