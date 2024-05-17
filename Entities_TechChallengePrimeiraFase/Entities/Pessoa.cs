namespace Entities_TechChallengePrimeiraFase.Entities
{
    public class Pessoa
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }      
        public required ICollection<ContatoPessoa> ContatoPessoas { get; set; }
    }
}
