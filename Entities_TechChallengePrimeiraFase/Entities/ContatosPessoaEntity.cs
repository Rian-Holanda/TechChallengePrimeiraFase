namespace Entities_TechChallengePrimeiraFase.Entities
{
    public class ContatosPessoaEntity
    {
        private int _id;
        private int _numero;
        private int _idPessoa;
        private int _idRegiao;

        public int Id { get { return _id; } set { _id = value; } }
        public int Numero { get { return _numero; } set { _numero = value; } }
        public int IdPessoa { get { return _idPessoa; } set { _idPessoa = value; } }
        public int IdRegiao { get { return _idRegiao; } set { _idRegiao = value; } }
        public required PessoasEntity Pessoa { get; set; }       
        public required RegioesEntity Regiao { get; set; }

    }
}
