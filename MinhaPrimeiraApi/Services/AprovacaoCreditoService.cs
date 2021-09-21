using MinhaPrimeiraApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Services
{
    public class AprovacaoCreditoService : IAprovacaoCreditoService
    {
        public decimal analiseDeCredito(DadosPessoaAnalise pessoa)
        {

            if (pessoa.RendaMensal < 1000)
                return 0;
            if (pessoa.RendaMensal >= 1000 && pessoa.RendaMensal <= 5000 && pessoa.QtdeFilhos == 0 )
                return 30;
            if (pessoa.RendaMensal >= 1000 && pessoa.RendaMensal <= 5000 && pessoa.QtdeFilhos <= 2)
                return 20;
            if (pessoa.RendaMensal >= 1000 && pessoa.RendaMensal <= 5000 && pessoa.QtdeFilhos > 2)
                return 10;
            if (pessoa.RendaMensal >  5000 )
                return 35;
            return 0;
        }
    }
}
