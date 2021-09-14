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
        public float peso { get;  set; }
        public float altura { get;  set; }
        public string Cpf { get;  set; }
        public int idade { get;  set; }
        public string sexo { get;  set; }
    }
}
