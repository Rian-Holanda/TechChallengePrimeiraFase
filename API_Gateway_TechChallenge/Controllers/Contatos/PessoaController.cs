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
using Newtonsoft.Json;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Contatos.Interface;

namespace API_Gateway_TechChallenge.Controllers.Contatos
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaProducer _pessoa;
        private ValidaPessoa validaPessoa = new ValidaPessoa();

        public PessoaController(IPessoaProducer pessoa)
        {
            _pessoa = pessoa;
        }


        #region Producer

        [HttpGet("GetPessoasProducer")]
        public async Task<IActionResult> GetPessoasProducerAsync()
        {
            //var pessoas = await _pessoa.GetPessoas();


                return Ok();
        }


        #endregion

        #region Consumer


        //[HttpGet("GetPessoas")]
        //public IActionResult GetPessoas()
        //{
        //    List<PessoasModel> pessoasModels = new List<PessoasModel>();

        //    var pessoas = _pessoasCommand.GetPessoas();

        //    if (pessoas != null)
        //    {
        //        pessoasModels.AddRange(pessoas.Select(p => new PessoasModel()
        //        {
        //            Id = p.Id,
        //            Nome = p.Nome,
        //            Email = p.Email,
        //        }));

        //        return Ok(pessoasModels);
        //    }
        //    else
        //    {
        //        return NoContent();
        //    }
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetPessoa(int id)
        //{
        //    var pessoa = _pessoasCommand.GetPessoa(id);

        //    if (pessoa != null)
        //    {
        //        PessoasModel pessoasModel = new PessoasModel()
        //        {
        //            Id = pessoa.Id,
        //            Nome = pessoa.Nome,
        //            Email = pessoa.Email
        //        };

        //        return Ok(pessoasModel);
        //    }
        //    else
        //    {
        //        return NoContent();
        //    }
        //}

        [HttpPost("InserirPessoa")]
        public async Task<IActionResult> InserirPessoa([FromBody] PessoasModel pessoasModel)
        {

            //PessoasEntity pessoa = new PessoasEntity()
            //{
            //    Nome = pessoasModel.Nome,
            //    Email = pessoasModel.Email
            //};

            //var result = await _pessoa.InserirPessoa(JsonConvert.SerializeObject(pessoa));

            //if (result) 
            //{
            //    return Ok("Sucesso");
            //}

            return Ok();

            //else 
            //{
            //    return BadRequest();
            //}

        }

        //[HttpPut("AlterarPessoa/id")]
        //public IActionResult AlterarPessoa(int id, [FromBody] PessoasModel pessoasModel)
        //{
        //    var pessoaEntity = _pessoasCommand.GetPessoa(id);

        //    if (pessoaEntity is not null)
        //    {
        //        pessoaEntity.Nome = pessoasModel.Nome;
        //        pessoaEntity.Email = pessoasModel.Email;

        //        if (_validaEmailPessoa.ValidaEmail(pessoaEntity.Email))
        //        {
        //            var resultValidacao = validaPessoa.Validate(pessoaEntity);

        //            var result = _pessoasCommand.AlterarPessoa(pessoaEntity, id);

        //            if (result)
        //            {
        //                return Ok();
        //            }
        //            else
        //            {
        //                return NoContent();
        //            }
        //        }
        //        return NoContent();
        //    }

        //    return NoContent();

        //}

        //[HttpDelete("DeletarPessoa/id")]
        //public IActionResult DeletarPessoa(int id)
        //{
        //    var pessoaEntity = _pessoasCommand.GetPessoa(id);

        //    if (pessoaEntity is not null)
        //    {
        //        var result = _pessoasCommand.ExcluirPessoa(pessoaEntity.Id);

        //        if (result)
        //        {
        //            return Ok();
        //        }
        //        else
        //        {
        //            return NoContent();
        //        }
        //    }
        //    return NoContent();
        //}

        #endregion

    }
}
