using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinhaPrimeiraApi.Context;
using MinhaPrimeiraApi.Models;
using MinhaPrimeiraApi.Validations;
using MongoDB.Driver;

namespace MinhaPrimeiraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {        
        private readonly PessoaContext Context;
        public PessoaController()
        {
            Context = new PessoaContext();
        }

        [HttpGet]
        public ActionResult OlaMundo()
        {
            return Ok();
        }

        
        [Authorize]
        [HttpGet("ObterPorCpf/{cpf}")]
        public ActionResult ObterPorCpf(string cpf)
        {
            return Ok( Context._pessoas.Find<Pessoa>(p => p.Cpf == cpf).FirstOrDefault());                
        }

        [HttpPost]
        public string Imc([FromBody] Pessoa pessoa)
        {
            return  (pessoa.Peso / (pessoa.Altura * pessoa.Altura)).ToString();
        }

        [HttpPost("Adicionar")]
        [ProducesResponseType(typeof(Pessoa), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult Adicionar(Pessoa pessoa)
        {

            PessoaValidation validator = new PessoaValidation();

            ValidationResult results = validator.Validate(pessoa);

            if (!results.IsValid)
            {
                List<String> erro = new List<string>();
                foreach (var failure in results.Errors)
                {
                    erro.Add("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return BadRequest(erro);
            }

            Context._pessoas.InsertOne(pessoa);
            
            return CreatedAtAction(nameof(Adicionar), "");
        }

        [HttpPut("Atualizar/{id}")]
        public ActionResult Atualizar(string id,[FromBody] Pessoa pessoa)
        {
            var pResultado = Context._pessoas.Find<Pessoa>(p => p.Id == id).FirstOrDefault();
            if (pResultado == null) return 
                    NotFound("Id não encontrado, atualizacao não realizada!");
            
            pessoa.Id = id;
            Context._pessoas.ReplaceOne<Pessoa>(p => p.Id == id, pessoa);
     
            return NoContent();

        }


        [HttpDelete("Remover/{id}")]
        public ActionResult Remover(string id)
        {
            var pResultado = Context._pessoas.Find<Pessoa>(p => p.Id == id).FirstOrDefault();
            if (pResultado == null) return
                    NotFound("Id não encontrado, atualizacao não realizada!");

            Context._pessoas.DeleteOne<Pessoa>(filter => filter.Id == id);
            return NoContent();
        }
    }
}
