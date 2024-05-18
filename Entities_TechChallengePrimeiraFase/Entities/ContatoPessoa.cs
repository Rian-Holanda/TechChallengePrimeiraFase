namespace Entities_TechChallengePrimeiraFase.Entities
{
    public class ContatoPessoa
    {
        public int Id { get; set; }
        public int Numero { get; set; }       
        public int IdPessoa { get; set; }
        public int IdRegiao { get; set; }
        public required Pessoa Pessoa { get; set; }       
        public required Regioes Regiao { get; set; }

    }
}
