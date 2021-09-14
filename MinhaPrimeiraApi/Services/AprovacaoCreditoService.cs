using MinhaPrimeiraApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Services
{
    public class AprovacaoCreditoService : IAprovacaoCreditoService
    {
        public decimal analiseDeCredito(Pessoa pessoa)
        {
            if (pessoa.idade < 18)
                return 0;
            if (pessoa.idade >= 18 && pessoa.idade <= 65)
                return 100;
            return 50;
        }
    }
}
