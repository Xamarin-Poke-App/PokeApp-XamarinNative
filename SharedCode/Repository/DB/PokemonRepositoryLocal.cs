using Newtonsoft.Json;
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
        public IPokemonRepository Repository;

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

        public async Task UpdatePokemonInfo(PokemonSpecie pokemon, PokemonLocal pokemonLocal)
        {
            pokemonLocal.BaseHappiness = pokemon.BaseHappiness;
            pokemonLocal.Generation = pokemon.Generation.Name;
            pokemonLocal.EvolutionChainId = pokemon.EvolutionChain.GetEvolutionChainIdFromUrl();
            pokemonLocal.FlavorTextEntry = pokemon.FlavorTextEntries.FirstOrDefault().FlavorText ?? "";
            if (pokemon.Habitat != null)
                pokemonLocal.Habitat = pokemon.Habitat.Name;

            await DatabaseManager.UpdateDataAsync<PokemonLocal>(pokemonLocal, DBModels.Pokemon.ToString());
        }

        public async Task<Result<PokemonLocal>> GetPokemonByIdLocalAsync(int pokeId)
        {
            if (await DatabaseManager.checkTableExistsAsync(DBModels.Pokemon.ToString()))
            {
                return await DatabaseManager.GetDataByIdAsync<PokemonLocal>(pokeId, DBModels.Pokemon.ToString());
            }
            return Result.Fail<PokemonLocal>("Can't get info from db");
        }
        
        public async Task StoreEvolutionChainAsync(EvolutionChainResponse evolutionChain)
        {
            var evolutionChainString = Newtonsoft.Json.JsonConvert.SerializeObject(evolutionChain);
            var localEvolutionChain = new EvolutionChainLocal { Id = evolutionChain.Id, Chain = evolutionChainString };
            await DatabaseManager.StoreDataAsync(localEvolutionChain, Constants.EvolutionChainTable);
        }

        public async Task<Result<EvolutionChainResponse>> GetEvolutionChainByIdFromLocalAsync(int id)
        {
            try
            {
                var dbResponse = await DatabaseManager.GetAllDataAsync<EvolutionChainLocal>();

                if (dbResponse.IsFailure)
                {
                    return Result.Fail<EvolutionChainResponse>("Could not get data");
                }
                var row = dbResponse.Value.Where(r => r.Id == id).First();

                EvolutionChainResponse evolutionChainResponse = JsonConvert.DeserializeObject<EvolutionChainResponse>(row.Chain);
                var evolutionChainFromLocal = new EvolutionChainResponse { Id = row.Id, Chain = evolutionChainResponse.Chain };

                return Result.Ok<EvolutionChainResponse>(evolutionChainFromLocal);
            } catch (Exception)
            {
                return Result.Fail<EvolutionChainResponse>("Could not get data");
            }
        }

        public async Task<Result<List<EvolutionChainResponse>>> GetAllEvolutionChainFromLocalAsync()
        {
            var dbResponse = await DatabaseManager.GetAllDataAsync<EvolutionChainLocal>();

            if (dbResponse.IsFailure)
            {
                return Result.Fail<List<EvolutionChainResponse>>("Could not get all data");
            }

            var rowsFromLocal = new List<EvolutionChainResponse>();
            foreach (var row in dbResponse.Value)
            {
                EvolutionChainResponse evolutionChainResponse = JsonConvert.DeserializeObject<EvolutionChainResponse>(row.Chain);
                var evolutionChainFromLocal = new EvolutionChainResponse { Id = row.Id, Chain = evolutionChainResponse.Chain };
                rowsFromLocal.Add(evolutionChainFromLocal);
            }

            return Result.Ok<List<EvolutionChainResponse>>(rowsFromLocal);
        }
    }
}
