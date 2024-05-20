namespace Entities_TechChallengePrimeiraFase.Entities
{
    public class ContatosPessoaEntity
    {
        private int _id;
        private string? _numero;
        private int _idPessoa;
        private int _idRegiao;
        private int _tipoContatoPessoa;

        public int Id { get { return _id; } set { _id = value; } }
        public string? Numero { get { return _numero; } set { _numero = value; } }
        public int IdPessoa { get { return _idPessoa; } set { _idPessoa = value; } }
        public int IdRegiao { get { return _idRegiao; } set { _idRegiao = value; } }
        public int TipoContatoPessoa { get { return _tipoContatoPessoa; } set { _tipoContatoPessoa = value; } }
        public required PessoasEntity Pessoa { get; set; }       
        public required RegioesEntity Regiao { get; set; }

    }
}
