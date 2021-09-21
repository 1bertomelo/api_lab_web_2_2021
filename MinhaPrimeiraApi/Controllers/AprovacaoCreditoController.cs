using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinhaPrimeiraApi.Infra.Email;
using MinhaPrimeiraApi.Models;
using MinhaPrimeiraApi.Services;

namespace MinhaPrimeiraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AprovacaoCreditoController : ControllerBase
    {
        private readonly IAprovacaoCreditoService _IaprovacaoCredito ;
        private readonly IEmailService _IEmailService;

        public AprovacaoCreditoController(IAprovacaoCreditoService IaprovacaoCredito, IEmailService IemailService)
        {
            _IaprovacaoCredito = IaprovacaoCredito;
            _IEmailService = IemailService;
        }

        [HttpPost("simularCredito")]
        public ActionResult simularCredito([FromBody] DadosPessoaAnalise p)
        {
            decimal valor =  _IaprovacaoCredito.analiseDeCredito(p);
            Mensagem mensagem = new Mensagem()
            {
                Assunto = "Simulação de crédito",
                CorpoMensagem = $"Resultado da sua avaliação foi de R$ {valor} .",
                De = "humberto.melo@gmail.com.br",
                Para = p.Email
            };                
            _IEmailService.Enviar(mensagem);
            return Ok(valor);
        }

    }
}
