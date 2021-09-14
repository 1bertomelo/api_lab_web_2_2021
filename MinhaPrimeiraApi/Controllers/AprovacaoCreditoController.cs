using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinhaPrimeiraApi.Models;
using MinhaPrimeiraApi.Services;

namespace MinhaPrimeiraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AprovacaoCreditoController : ControllerBase
    {
        private readonly IAprovacaoCreditoService _IaprovacaoCredito ;

        public AprovacaoCreditoController(IAprovacaoCreditoService IaprovacaoCredito)
        {
            _IaprovacaoCredito = IaprovacaoCredito;
        }

        [HttpPost("simularCredito")]
        public ActionResult simularCredito([FromBody] Pessoa p)
        {
            decimal valor =  _IaprovacaoCredito.analiseDeCredito(p);
            return Ok(valor);
        }

    }
}
