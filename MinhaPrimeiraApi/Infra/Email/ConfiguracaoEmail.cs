using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Infra.Email
{
    public class ConfiguracaoEmail
    {
        public string HostServidor { get;  set; }
        public int Porta { get;  set; }
        public bool HabilitaSsl { get;  set; }
        public string Usuario { get;  set; }
        public string Senha { get;  set; }

        public string NomeEmissor { get;  set; }
    }
}
