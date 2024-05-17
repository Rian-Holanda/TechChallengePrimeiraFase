using Entities_TechChallengePrimeiraFase.Entities;

namespace Entities_TechChallengePrimeiraFase.Entities
{
    public class Regiao
    {
        //public int Id { get; set; }
        //public required string Sigla { get; set; }    
        //public required ICollection<ContatoPessoa> Contatos { get; set; }

        public int Id { get; set; }
        public string Sigla { get; set; }

        // Propriedades de navegação
        public ICollection<RegiaoCodigoArea> RegiaoCodigoAreas { get; set; }


    }
}


