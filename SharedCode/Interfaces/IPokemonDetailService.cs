using System;
using SharedCode.Model.DB;
using SharedCode.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharedCode.Model.Api;

namespace SharedCode.Interfaces
{
	public interface IPokemonDetailService
	{
        Task<Result<PokemonLocal>> UpdateOrGetPokemonByIdLocalAsync(int pokeId);
        Task<Result<byte[]>> GetPokemonImage(int pokeId);
        Task<Result<EvolutionChainResponse>> UpdateOrGetEvolutionChainByPokemonId(int id);
    }
}

