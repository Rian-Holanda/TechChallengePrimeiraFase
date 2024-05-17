namespace Entities_TechChallengePrimeiraFase.Entities
{
    public class RegiaoCodigoArea
    {
        public int Id { get; set; }
        public int DDD { get; set; }       
        public int IdRegiao { get; set; }
        public required Regiao Regiao { get; set; }
    }
}
