using System;
using SharedCode.Model.DB;
using SharedCode.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedCode.Repository.Interfaces
{
	public interface IPokemonRepositoryLocal
	{
        Task StorePokemonListAsync(List<PokemonLocal> pokemons);
        Task<Result<List<PokemonLocal>>> GetPokemonLocalListAsync();
    }
}

