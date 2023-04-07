using System;
using SharedCode.Model.DB;
using SharedCode.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedCode.Interfaces
{
	public interface IPokemonService
	{
        Task<Result<List<PokemonLocal>>> GetPokemonDataAsync();
    }
}

