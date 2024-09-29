﻿using Business_TechChallengePrimeiraFase.Contatos.Application.Interfaces;
using DataAccess_TechChallengePrimeiraFase.Contatos.Interfaces;
using Entities_TechChallengePrimeiraFase.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using DataAccess_TechChallengePrimeiraFase.Contatos.Interface;
using API_Producer_TechChallenge.Models.Pessoa;
using Business_TechChallengePrimeiraFase.Contatos.Domain;
using Newtonsoft.Json;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Gateway;
using Infrastructure_TechChallengePrimeiraFase.Util.Rabbit.Contatos.Interface;

namespace API_Producer_TechChallenge.Controllers.Contatos
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoasQueries _pessoasQueries;
        private readonly IPessoasCommand _pessoasCommand;
        private readonly IValidaEmailPessoa _validaEmailPessoa;
        private readonly IPessoaProducer _pessooaProducer;

        private ValidaPessoa validaPessoa = new ValidaPessoa();

        public PessoaController(IPessoasQueries pessoasQueries, IPessoasCommand pessoasCommand, IValidaEmailPessoa validaEmailPessoa, IPessoaProducer pessoaProducer)
        {

            _pessoasCommand = pessoasCommand;
            _pessoasQueries = pessoasQueries;
            _validaEmailPessoa = validaEmailPessoa;
            _pessooaProducer = pessoaProducer;
        }

        [HttpGet("GetPessoas")]
        public IActionResult GetPessoas()
        {
            List<PessoasModel> pessoasModels = new List<PessoasModel>();

            var pessoas = _pessoasCommand.GetPessoas();

            if (pessoas != null)
            {
                pessoasModels.AddRange(pessoas.Select(p => new PessoasModel()
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Email = p.Email,
                }));

                return Ok(pessoasModels);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPessoa(int id)
        {
            var pessoa = _pessoasCommand.GetPessoa(id);

            if (pessoa != null)
            {
                PessoasModel pessoasModel = new PessoasModel()
                {
                    Id =pessoa.Id,
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
        public IActionResult InserirPessoa([FromBody] PessoasModel pessoasModel)
        {
            
            PessoaEntity pessoa = new PessoaEntity() 
            { 
               Nome = pessoasModel.Nome,
               Email = pessoasModel.Email
            };

            if (_validaEmailPessoa.ValidaEmail(pessoa.Email))
            {
                var resultValidacao = validaPessoa.Validate(pessoa);

                var result = _pessooaProducer.InserirPessoa(JsonConvert.SerializeObject(pessoa));

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

        [HttpPut("AlterarPessoa/id")]
        public IActionResult AlterarPessoa([FromBody] PessoasModel pessoasModel)
        {
            var pessoaEntity = _pessoasCommand.GetPessoa(pessoasModel.Id);

            if (pessoaEntity is not null)
            {
                pessoaEntity.Nome = pessoasModel.Nome;
                pessoaEntity.Email = pessoasModel.Email;

                if (_validaEmailPessoa.ValidaEmail(pessoaEntity.Email))
                {
                    var resultValidacao = validaPessoa.Validate(pessoaEntity);

                    var result =  _pessooaProducer.AlterarPessoa(JsonConvert.SerializeObject(pessoaEntity)); ;

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
                var result = _pessooaProducer.ExcluirPessoa(pessoaEntity.Id.ToString());

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
