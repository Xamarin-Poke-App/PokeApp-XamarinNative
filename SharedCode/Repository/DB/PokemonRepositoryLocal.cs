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

        public PokemonRepositoryLocal(IDatabaseManager databaseManager, IPokemonRepository repository)
		{
            this.Repository = repository;
			this.DatabaseManager = databaseManager;
		}

		public async Task BuildPokemonLocalList()
		{
            if (DatabaseManager.checkTableExists(DBModels.Pokemon.ToString()))
				return;

			var data = await Repository.GetPokemonList();

			if (data.Success)
			{
                Dictionary<int, PokemonLocal> pokemons = data.Value.Select(pokemon => new PokemonLocal(pokemon.name, pokemon.GetIdFromUrl())).ToDictionary(x => x.Id, x => x);
				var typesList = await Repository.GetPokemonTypesList();
				if (typesList.Success)
				{
                    foreach (var type in typesList.Value)
					{
						var typeInfo = await Repository.GetTypeInfo(type.GetIdFromUrl());
						if (typeInfo.Success)
						{
							pokemons = PopulateTypeForPokemons(pokemons, typeInfo.Value.pokemon, typeInfo.Value.name);
						}
					}
					
                }

				var generationsList = await Repository.GetPokemonGenerationList();
				if (generationsList.Success)
				{
					foreach (var generation in generationsList.Value)
					{
						var generationInfo = await Repository.GetGenerationInfo(generation.GetIdFromUrl());
						if (generationInfo.Success)
						{
                            pokemons = PopulateRegionForPokemons(pokemons, generationInfo.Value.pokemon_species, generationInfo.Value.main_region.name);
						}
					}
				}

                foreach (var finalPokemon in pokemons)
                {
                    DatabaseManager.StoreData<PokemonLocal>(finalPokemon.Value, DBModels.Pokemon.ToString());
                }
            }
		}

        public Dictionary<int, PokemonLocal> PopulateTypeForPokemons(Dictionary<int, PokemonLocal> pokemonsList, List<Pokemon> pokemonTypeList, string TypeName)
		{
			var pokemonTypeListIndex = 0;
			var auxPokemonList = pokemonsList;
			while(pokemonTypeListIndex < pokemonTypeList.Count())
			{
                var id = pokemonTypeList[pokemonTypeListIndex].pokemon.GetIdFromUrl();
                if (id > 1010)
                {
                    pokemonTypeListIndex++;
                    continue;
                }
                if (auxPokemonList[id].Types == "")
                    auxPokemonList[id].Types += TypeName;
                else
                    auxPokemonList[id].Types += $"%{TypeName}";
				pokemonTypeListIndex++;
			}

			return auxPokemonList;
		}

        public Dictionary<int, PokemonLocal> PopulateRegionForPokemons(Dictionary<int, PokemonLocal> pokemonsList, List<ResultItem> pokemonSpeciesList, string regionName)
        {
            var pokemonSpeciesListIndex = 0;
            var auxPokemonList = pokemonsList;
            while (pokemonSpeciesListIndex < pokemonSpeciesList.Count())
            {
                var id = pokemonSpeciesList[pokemonSpeciesListIndex].GetIdFromUrl();
                auxPokemonList[id].Region = regionName;

                pokemonSpeciesListIndex++;
            }

            return auxPokemonList;
        }

        public async Task<Result<List<PokemonLocal>>> GetPokemonLocalListAsync()
        {
            await BuildPokemonLocalList();
            if (DatabaseManager.checkTableExists(DBModels.Pokemon.ToString()))
            {
                return DatabaseManager.GetAllData<PokemonLocal>();
            }
            return Result.Fail<List<PokemonLocal>>("Can't get info from db");
        }

        public void StoreEvolutionChain(EvolutionChainResponse evolutionChain)
        {
            var evolutionChainString = Newtonsoft.Json.JsonConvert.SerializeObject(evolutionChain);
            var localEvolutionChain = new EvolutionChainLocal { Id = evolutionChain.id, Chain = evolutionChainString };
            DatabaseManager.StoreData(localEvolutionChain, Constants.EvolutionChainTable);
        }

        public Result<EvolutionChainResponse> GetEvolutionChainByIdFromLocal(int id)
        {
            try
            {
                var dbResponse = DatabaseManager.GetAllData<EvolutionChainLocal>();

                if (dbResponse.IsFailure)
                {
                    return Result.Fail<EvolutionChainResponse>("Could not get data");
                }
                var row = dbResponse.Value.Where(r => r.Id == id).First();

                EvolutionChainResponse evolutionChainResponse = JsonConvert.DeserializeObject<EvolutionChainResponse>(row.Chain);
                var evolutionChainFromLocal = new EvolutionChainResponse { id = row.Id, chain = evolutionChainResponse.chain };

                return Result.Ok<EvolutionChainResponse>(evolutionChainFromLocal);
            } catch (Exception)
            {
                return Result.Fail<EvolutionChainResponse>("Could not get data");
            }
        }

        public Result<List<EvolutionChainResponse>> GetAllEvolutionChainFromLocal()
        {
            var dbResponse = DatabaseManager.GetAllData<EvolutionChainLocal>();

            if (dbResponse.IsFailure)
            {
                return Result.Fail<List<EvolutionChainResponse>>("Could not get all data");
            }

            var rowsFromLocal = new List<EvolutionChainResponse>();
            foreach (var row in dbResponse.Value)
            {
                EvolutionChainResponse evolutionChainResponse = JsonConvert.DeserializeObject<EvolutionChainResponse>(row.Chain);
                var evolutionChainFromLocal = new EvolutionChainResponse { id = row.Id, chain = evolutionChainResponse.chain };
                rowsFromLocal.Add(evolutionChainFromLocal);
            }

            return Result.Ok<List<EvolutionChainResponse>>(rowsFromLocal);
        }
    }
}
