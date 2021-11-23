using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Extensions.Hosting.Internal;

namespace MinhaPrimeiraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {        
        private readonly PessoaContext Context;
        public static IWebHostEnvironment _environment;
        public PessoaController(IWebHostEnvironment environment)
        {
            Context = new PessoaContext();
            _environment = environment;
        }

        [HttpGet]
        public ActionResult OlaMundo()
        {
            return Ok();
        }


        /// <summary>
        /// Consulta dados de uma pessoa a partir do CPF
        /// Requer uso de token.
        /// </summary>
        /// <param name="cpf">CPF</param>
        /// <returns>Objeto contendo os dados de uma pessoa.</returns>
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

        [HttpPost("upload")]
        public async Task<ActionResult> EnviaArquivo([FromForm] IFormFile arquivo)
        {
            if (arquivo.Length > 0)
            {
				if( arquivo.ContentType != "image/jpeg" &&
                    arquivo.ContentType != "image/jpg" &&
                    arquivo.ContentType != "image/png"
                   )
				{
                    return BadRequest("Formato Inválido de imagens");
				}

                try
                {
                   
                    string contentRootPath = _environment.ContentRootPath;
                    string path = "";
                    path = Path.Combine(contentRootPath, "imagens");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream filestream = System.IO.File.Create(path + arquivo.FileName))
                    {
                        await arquivo.CopyToAsync(filestream);
                        filestream.Flush();
                        return Ok("Imagem enviada com sucesso " + arquivo.FileName);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }
            else
            {
                return BadRequest("Ocorreu uma falha no envio do arquivo...");
            }
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
