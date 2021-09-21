using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Infra.Email
{
    public class Mensagem
    {
        public string De { get;  set; }
        public string Para { get;  set; }
        public string Assunto { get;  set; }
        public string CorpoMensagem { get;  set; }
    }
}
