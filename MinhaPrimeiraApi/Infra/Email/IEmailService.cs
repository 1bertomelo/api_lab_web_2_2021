using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Infra.Email
{
    public interface IEmailService
    {
        Task Enviar(Mensagem mensagem);
    }
}
