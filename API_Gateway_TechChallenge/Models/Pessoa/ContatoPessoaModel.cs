﻿namespace API_Gateway_TechChallenge.Models.Pessoa
{
    public class ContatoPessoaModel
    {
        public int IdPessoa { get; set; }
        public string? Numero { get; set; }
        public string? SiglaRegiao { get; set; }
        public bool ContatoCelular { get; set; }
    }
}
