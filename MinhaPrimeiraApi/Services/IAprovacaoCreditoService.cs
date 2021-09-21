using MinhaPrimeiraApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Services
{
    public interface IAprovacaoCreditoService
    {
        public decimal analiseDeCredito(DadosPessoaAnalise pessoa);
    }
}
