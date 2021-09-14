using FluentValidation;
using MinhaPrimeiraApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Validations
{
    public class PessoaValidation : AbstractValidator<Pessoa>
    {
        public PessoaValidation()
        {
            RuleFor(pessoa => pessoa.Nome).NotNull().WithMessage("Nome é obrigatorio");

        }
    }

    
}
