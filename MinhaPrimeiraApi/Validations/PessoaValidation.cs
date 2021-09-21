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
            RuleFor(pessoa => pessoa.Nome)
                .NotNull().WithMessage("Nome é obrigatório")
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MinimumLength(3).WithMessage("Informe no mínimo 3 letras no nome")
                .MaximumLength(255).WithMessage("Informe no máximo 255 letras no nome");
            
            RuleFor(pessoa => pessoa.Cpf)
                .NotNull().WithMessage("Cpf é obrigatório")
                .NotEmpty().WithMessage("Cpf é obrigatório")
                .Must(validaCpf).WithMessage("Cpf inválido")
                .Length(11).WithMessage("Cpf tamanho deve ser de 11 digitos");

            RuleFor(pessoa => pessoa.Idade)
            .NotNull().WithMessage("Idade é obrigatório")
            .NotEmpty().WithMessage("Idade é obrigatório")
            .GreaterThanOrEqualTo(18).WithMessage("Idade deve ser  maior igual de 18 anos");

            RuleFor(pessoa => pessoa.Peso)
            .NotNull().WithMessage("Peso é obrigatório")
            .NotEmpty().WithMessage("Peso é obrigatório")
            .GreaterThan(0).WithMessage("Peso deve ser  maior que 0 KG");

            RuleFor(pessoa => pessoa.Altura)
            .NotNull().WithMessage("Altura é obrigatória")
            .NotEmpty().WithMessage("Altura é obrigatória")
            .GreaterThan(0).WithMessage("Peso deve ser  maior que 0 Metros");

            RuleFor(pessoa => pessoa.Sexo)
            .NotNull().WithMessage("Sexo é obrigatório")
            .NotEmpty().WithMessage("Sexo é obrigatório")
            .Must(validaSexo).WithMessage("Opção invalida: Informa M, F ou Não Informado");

        }

        private  static bool validaSexo(string sexo)
        {
            switch (sexo)
            {
                case "M":
                case "m": return true;
                case "F":
                case "f": return true;
                case "Não Informado": return true;
                default:
                    return false;
            }
        }

        private static bool validaCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);

        }
    }

    
}
