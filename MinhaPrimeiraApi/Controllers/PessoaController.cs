using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinhaPrimeiraApi.Models;
using MinhaPrimeiraApi.Validations;

namespace MinhaPrimeiraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IList<Pessoa> pessoas;

        public PessoaController()
        {
            pessoas = new List<Pessoa>();
            pessoas.Add(new Pessoa() {Id = 1, Nome = "H1", Altura=1.67F, Cpf="123", Peso=56 });
            pessoas.Add(new Pessoa() {Id = 2, Nome = "Maria", Altura = 1.2F, Cpf = "456", Peso = 23 });
            pessoas.Add(new Pessoa() {Id = 3, Nome = "Nicholas", Altura = 1.05F, Cpf = "789", Peso = 16 });
            pessoas.Add(new Pessoa() {Id = 4, Nome = "Anastacia", Altura = 1.5F, Cpf = "345", Peso = 56 });

        }

        [HttpGet]
        public string OlaMundo(string nome)
        {
            return $"Olá Mundo {nome}!";
        }

        [HttpGet("ObterPorCpf/{cpf}")]
        public ActionResult ObterPorCpf(string cpf)
        {
            var resultadoPessoa = pessoas.Where(p => p.Cpf == cpf).FirstOrDefault();
            if (resultadoPessoa == null) return BadRequest();
            return Ok(resultadoPessoa);
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
                pessoas.Add(pessoa);
            return CreatedAtAction(nameof(Adicionar), pessoa);
        }

        [HttpPut("Atualizar/{id}")]
        public ActionResult Atualizar(int id,[FromBody] Pessoa pessoa)
        {
            var resultadoPessoa = pessoas.Where(p => p.Id == id).FirstOrDefault();
            if (resultadoPessoa == null)
            {
                return NotFound();
            }
            pessoa.Id = id;
            pessoas.Remove(resultadoPessoa);
            pessoas.Add(pessoa);

            return NoContent();

        }


        [HttpDelete("Remover/{id}")]
        public ActionResult Remover(int id)
        {
            var resultadoPessoa = pessoas.Where(p => p.Id == id).FirstOrDefault();
            if (resultadoPessoa == null)
            {
                return NotFound();
            }
            pessoas.Remove(resultadoPessoa);
            return NoContent();
        }
    }
}
