using System;
using SharedCode.Model.DB;
using SharedCode.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharedCode.Model.Api;
using SharedCode.Model;

namespace SharedCode.Repository.Interfaces
{
	public interface IPokemonRepositoryLocal
	{
        Task StorePokemonListAsync(List<PokemonLocal> pokemons);
        Task<Result<List<PokemonLocal>>> GetPokemonLocalListAsync();
        Task UpdatePokemonInfo(PokemonSpecie pokemon, PokemonLocal pokemonLocal);
        Task<Result<PokemonLocal>> GetPokemonByIdLocalAsync(int pokeId);
        Task StoreEvolutionChainAsync(EvolutionChainResponse evolutionChain);
        Task<Result<EvolutionChainResponse>> GetEvolutionChainByIdFromLocalAsync(int id);
        Task<Result<List<EvolutionChainResponse>>> GetAllEvolutionChainFromLocalAsync();
    }
}

