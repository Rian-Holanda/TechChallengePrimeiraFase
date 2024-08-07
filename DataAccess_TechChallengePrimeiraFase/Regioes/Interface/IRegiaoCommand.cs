﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_TechChallengePrimeiraFase.Entities;

namespace DataAccess_TechChallengePrimeiraFase.Regioes.Interface
{
    public interface IRegiaoCommand
    {

        int InserirRegiao(RegioesEntity regioesEntity);
        bool AlterarRegiao(RegioesEntity regioesEntity, int idRegiao);
        bool ExcluirRegiao(int idRegiao);
        RegioesEntity? GetRegiao(int idRegiao);
        List<RegioesEntity>? GetRegioes();
    }
}
