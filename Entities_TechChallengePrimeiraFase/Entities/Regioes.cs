using Entities_TechChallengePrimeiraFase.Entities;

namespace Entities_TechChallengePrimeiraFase.Entities
{
    public class Regioes
    {

        private int _id;
        private string? _sigla;

        public int Id { get { return _id; } set { _id = value; } }
        public string? Sigla { get { return _sigla; } set { _sigla = value; } }

        // Propriedades de navegação
        public ICollection<RegiaoCodigoArea>? RegiaoCodigoAreas { get; set; }


    }
}


