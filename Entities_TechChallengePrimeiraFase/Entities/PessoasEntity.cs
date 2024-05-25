namespace Entities_TechChallengePrimeiraFase.Entities
{
    public class PessoasEntity
    {
        private int _id;
        private string? _nome;
        private string? _email;

        public int Id { get { return _id; } set { _id = value; } }
        public  string? Nome { get { return _nome; } set { _nome = value; } }
        public  string? Email { get { return _email; } set { _email = value; } }
        public required ICollection<ContatosPessoaEntity> ContatoPessoas { get; set; }
    }
}
