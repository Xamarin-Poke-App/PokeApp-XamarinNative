using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedCode.Database;
using SharedCode.Model;
using SharedCode.Model.Api;
using SharedCode.Model.DB;
using SharedCode.Repository.Interfaces;
using SharedCode.Util;
using Unity;
using static SharedCode.Util.Enums;

namespace SharedCode.Repository.DB
{
	public class PokemonRepositoryLocal : IPokemonRepositoryLocal
	{
        [Dependency]
		public IDatabaseManager DatabaseManager;

        public PokemonRepositoryLocal(IDatabaseManager databaseManager)
		{
			this.DatabaseManager = databaseManager;
		}

        public async Task StorePokemonListAsync(List<PokemonLocal> pokemons)
        {
            if (await DatabaseManager.checkTableExistsAsync(DBModels.Pokemon.ToString()))
                return;

            foreach (var finalPokemon in pokemons)
            {
                await DatabaseManager.StoreDataAsync<PokemonLocal>(finalPokemon, DBModels.Pokemon.ToString());
            }
        }

        public async Task<Result<List<PokemonLocal>>> GetPokemonLocalListAsync()
        {
            if (await DatabaseManager.checkTableExistsAsync(DBModels.Pokemon.ToString()))
            {
                return await DatabaseManager.GetAllDataAsync<PokemonLocal>();
            }
            return Result.Ok<List<PokemonLocal>>(new List<PokemonLocal>());
            
        }
    }
}

