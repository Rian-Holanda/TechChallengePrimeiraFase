﻿namespace Entities_TechChallengePrimeiraFase.Entities
{
    public class PessoaEntity
    {
        private int _id;
        private string? _nome;
        private string? _email;

        public int Id { get { return _id; } set { _id = value; } }
        public  string? Nome { get { return _nome; } set { _nome = value; } }
        public  string? Email { get { return _email; } set { _email = value; } }
        public ICollection<ContatoPessoaEntity>? ContatoPessoa { get; set; }
    }
}
