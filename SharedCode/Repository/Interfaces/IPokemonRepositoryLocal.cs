using System;
using SharedCode.Model.DB;
using SharedCode.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharedCode.Model;

namespace SharedCode.Repository.Interfaces
{
	public interface IPokemonRepositoryLocal
	{
        Task<Result<List<PokemonLocal>>> GetPokemonLocalListAsync();
        void StoreEvolutionChain(EvolutionChainResponse evolutionChain);
        Result<EvolutionChainResponse> GetEvolutionChainByIdFromLocal(int id);
        Result<List<EvolutionChainResponse>> GetAllEvolutionChainFromLocal(int id);
    }
}

