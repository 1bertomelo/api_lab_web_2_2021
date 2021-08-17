using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinhaPrimeiraApi.Models;

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
            pessoas.Add(new Pessoa() { Nome = "H1", altura=1.67F, Cpf="123", peso=56 });
            pessoas.Add(new Pessoa() { Nome = "Maria", altura = 1.2F, Cpf = "456", peso = 23 });
            pessoas.Add(new Pessoa() { Nome = "Nicholas", altura = 1.05F, Cpf = "789", peso = 16 });
            pessoas.Add(new Pessoa() { Nome = "Anastacia", altura = 1.5F, Cpf = "345", peso = 56 });

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
            return  (pessoa.peso / (pessoa.altura * pessoa.altura)).ToString();
        }

        [HttpPost("Adicionar")]
        [ProducesResponseType(typeof(Pessoa), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult Adicionar(Pessoa pessoa)
        {
            pessoas.Add(pessoa);
            return CreatedAtAction(nameof(Adicionar), pessoa);
        }
    }
}
