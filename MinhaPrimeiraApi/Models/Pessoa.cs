using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Models
{
    public class Pessoa
    {
        public int Id { get;  set; }
        public string Nome { get;  set; }
        public float Peso { get;  set; }
        public float Altura { get;  set; }
        public string Cpf { get;  set; }
        public int Idade { get;  set; }
        public string Sexo { get;  set; }
        public decimal RendaMensal { get; set; }
        public int QtdeFilhos { get; set; }

    }
}
