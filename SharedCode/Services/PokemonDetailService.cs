using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharedCode.Database;
using SharedCode.Interfaces;
using SharedCode.Model.Api;
using SharedCode.Model.DB;
using SharedCode.Repository.Interfaces;
using SharedCode.Util;
using Unity;
using static SharedCode.Util.Enums;

namespace SharedCode.Services
{
	public class PokemonDetailService : IPokemonDetailService
	{
        [Dependency]
        public IPokemonRepositoryLocal RepositoryLocal;

        [Dependency]
        public IPokemonRepository Repository;

        NetworkConnection networkConnection = IocContainer.GetDependency<NetworkConnection>();

        public async Task<Result<PokemonLocal>> UpdateOrGetPokemonByIdLocalAsync(int pokeId)
        {
            var localData = await RepositoryLocal.GetPokemonByIdLocalAsync(pokeId);
            var isNetworkAvailable = networkConnection.GetIsConnectedCurrentStatus();

            if (localData.Success)
            {
                if (localData.Value.Generation != null)
                {
                    return localData;
                } else
                {
                    if (isNetworkAvailable)
                    {
                        var data = await Repository.GetPokemonSpecieInfo(pokeId);

                        if (data.Success)
                        {
                            await RepositoryLocal.UpdatePokemonInfo(data.Value, localData.Value);
                            return await RepositoryLocal.GetPokemonByIdLocalAsync(pokeId);
                        } else
                        {
                            return Result.Fail<PokemonLocal>(data.Error);
                        }
                    } else
                    {
                        return Result.Fail<PokemonLocal>("No Internet connection");
                    }
                }
            }
            else
            {
                return localData;
            }
        }

        public async Task<Result<byte[]>> GetPokemonImage(int pokeId)
        {
            var isNetworkAvailable = networkConnection.GetIsConnectedCurrentStatus();

            if (isNetworkAvailable)
            {
                return await Repository.GetPokemonImage(pokeId);
            } else
            {
                return Result.Fail<byte[]>("No Internet connection");
            }
        }

        public async Task<Result<EvolutionChainResponse>> GetEvolutionChainByPokemonId(int id)
        {
            var isNetworkAvailable = networkConnection.GetIsConnectedCurrentStatus();

            if (isNetworkAvailable)
            {
                return await Repository.GetEvolutionChainByPokemonId(id);
            }
            else
            {
                return Result.Fail<EvolutionChainResponse>("No Internet connection");
            }
        }
    }
}

