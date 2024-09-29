namespace Entities_TechChallengePrimeiraFase.Entities
{
    public class RegiaoCodigoAreaEntity
    {
        private int _id;
        private int _ddd;
        private int _idRegiao;


        public int Id { get { return _id; } set { _id = value; } }
        public int DDD { get { return _ddd; } set { _ddd = value; } }
        public int IdRegiao { get { return _idRegiao; } set { _idRegiao = value; } }
        public RegiaoEntity? Regiao { get; set; }
    }
}
