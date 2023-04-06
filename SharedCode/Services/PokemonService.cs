using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SharedCode.Helpers;
using SharedCode.Interfaces;
using SharedCode.Model;
using SharedCode.Model.Api;
using SharedCode.Model.DB;
using SharedCode.Repository.Interfaces;
using SharedCode.Util;
using Unity;

namespace SharedCode.Services
{
	public class PokemonService: IPokemonService
    {

        [Dependency]
        public IPokemonRepositoryLocal RepositoryLocal;

        [Dependency]
        public IPokemonRepository Repository;

        NetworkConnection networkConnection = IocContainer.GetDependency<NetworkConnection>();

        public async Task<Result<List<PokemonLocal>>> GetPokemonDataAsync()
        {
            
            var localData = await RepositoryLocal.GetPokemonLocalListAsync();
            var isNetworkAvailable = networkConnection.GetIsConnectedCurrentStatus();

            if (localData.Success)
            {
                if (localData.Value.Count() != 0)
                {
                    return localData;
                }
                else
                {

                    if (isNetworkAvailable)
                    {
                        var data = await CallPokemonListAndTypes();
                        if (data.Success)
                        {
                            await RepositoryLocal.StorePokemonListAsync(data.Value);
                        }
                        return data;
                    }
                    else
                    {
                        return Result.Fail<List<PokemonLocal>>("No Data");
                    }
                }
            }
            else
            {
                return localData;
            }
        }

        private async Task<Result<List<PokemonLocal>>> CallPokemonListAndTypes()
        {
            var data = await Repository.GetPokemonList();

            if (data.Success)
            {
                Dictionary<int, PokemonLocal> pokemons = data.Value.Select(pokemon => new PokemonLocal(pokemon.Name, pokemon.GetIdFromUrl())).ToDictionary(x => x.Id, x => x);
                var typesList = await Repository.GetPokemonTypesList();
                if (typesList.Success)
                {
                    foreach (var type in typesList.Value)
                    {
                        var typeInfo = await Repository.GetTypeInfo(type.GetIdFromUrl());
                        if (typeInfo.Success)
                        {
                            pokemons = PopulateTypeForPokemons(pokemons, typeInfo.Value.Pokemon, typeInfo.Value.Name);
                        }
                        else
                        {
                            return Result.Fail<List<PokemonLocal>>(typeInfo.Error);
                        }
                    }
                }
                else
                {
                    return Result.Fail<List<PokemonLocal>>(typesList.Error);
                }

                var generationsList = await Repository.GetPokemonGenerationList();
                if (generationsList.Success)
                {
                    foreach (var generation in generationsList.Value)
                    {
                        var generationInfo = await Repository.GetGenerationInfo(generation.GetIdFromUrl());
                        if (generationInfo.Success)
                        {
                            pokemons = PopulateRegionForPokemons(pokemons, generationInfo.Value.PokemonSpecies, generationInfo.Value.MainRegion.Name);
                        }
                        else
                        {
                            return Result.Fail<List<PokemonLocal>>(generationInfo.Error);
                        }
                    }
                    return Result.Ok<List<PokemonLocal>>(pokemons.Values.ToList());
                }
                else
                {
                    return Result.Fail<List<PokemonLocal>>(generationsList.Error);
                }
            }
            else
            {
                return Result.Fail<List<PokemonLocal>>(data.Error);
            }

        }

        private Dictionary<int, PokemonLocal> PopulateTypeForPokemons(Dictionary<int, PokemonLocal> pokemonsList, List<Pokemon> pokemonTypeList, string TypeName)
        {
            var pokemonTypeListIndex = 0;
            var auxPokemonList = pokemonsList;
            while (pokemonTypeListIndex < pokemonTypeList.Count())
            {
                var id = pokemonTypeList[pokemonTypeListIndex].PokemonItem.GetIdFromUrl();
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

    }
}

