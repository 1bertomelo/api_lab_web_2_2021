using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Models
{
    public class Pessoa
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get;  set; }
        public string Codigo { get; set; }
        public string Nome { get;  set; }
        [BsonRepresentation(BsonType.Decimal128, AllowTruncation = true)]
        public float Peso { get;  set; }
        [BsonRepresentation(BsonType.Decimal128, AllowTruncation = true)]
        public float Altura { get;  set; }
        public string Cpf { get;  set; }
        public int Idade { get;  set; }
        public string Sexo { get;  set; }
        [BsonRepresentation(BsonType.Decimal128, AllowTruncation = true)]
        public decimal RendaMensal { get; set; }
        public int QtdeFilhos { get; set; }

    }
}
