using System;
using MinhaPrimeiraApi.Models;
using MongoDB.Driver;

namespace MinhaPrimeiraApi.Context
{
    public class PessoaContext
    {
        public MongoDatabase Database;
        public String DataBaseName = "test";

        string conexaoMongoDB = "mongodb+srv://humberto:humberto@cluster0.fbd4q.azure.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";
        public IMongoCollection<Pessoa> _pessoas;
        public IMongoCollection<Usuario> _usuarios;
        //public IMongoCollection<Categoria> _categoria;

        public PessoaContext()
        {
            var cliente = new MongoClient(conexaoMongoDB);
            var server = cliente.GetDatabase(DataBaseName);
            _pessoas = server.GetCollection<Pessoa>("Pessoas");
            _usuarios = server.GetCollection<Usuario>("usuarios");
            //_categoria = server.GetCollection<Categoria>("Categorias");
        }
    }
}
