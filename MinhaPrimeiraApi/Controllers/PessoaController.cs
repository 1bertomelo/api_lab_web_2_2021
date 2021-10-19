using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinhaPrimeiraApi.Context;
using MinhaPrimeiraApi.Models;
using MinhaPrimeiraApi.Validations;

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

        [HttpGet("ObterPorCpf/{cpf}")]
        public ActionResult ObterPorCpf(string cpf)
        {
              return Ok();
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
        public ActionResult Atualizar(int id,[FromBody] Pessoa pessoa)
        {
     
            return NoContent();

        }


        [HttpDelete("Remover/{id}")]
        public ActionResult Remover(int id)
        {     
            return NoContent();
        }
    }
}
