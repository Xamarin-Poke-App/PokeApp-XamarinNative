using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedCode.Database;
using SharedCode.Model;
using SharedCode.Services;
using SharedCode.Model.Api;
using SharedCode.Model.DB;
using SharedCode.Repository.Interfaces;
using SharedCode.Util;

namespace SharedCode.Repository
{
	public class PokemonRepository : IPokemonRepository
	{
        private readonly INetworkHandler NetworkHandler;

		public PokemonRepository(INetworkHandler networkHandler)
		{
            NetworkHandler = networkHandler;
		}

        public async Task<Result<List<ResultItem>>> GetPokemonList()
        {
            try
            {
                var pokemons = await NetworkHandler.GetData<BasicListResponse>(Constants.PokemonAPIBaseAddress + "pokemon?limit=1008");
                return Result.Ok(pokemons.Results);
            }
            catch (NetworkErrorException ex)
            {
                // You can customize the error messages just checking the exceptionCode or just use the exceptionMessage instead (see default case)
                switch (ex.Code)
                {
                    case (int)HttpStatusCode.InternalServerError:
                        return Result.Fail<List<ResultItem>>("It seems like PokeApi server is down");
                    case (int)HttpStatusCode.NotFound:
                        return Result.Fail<List<ResultItem>>("The pokemon list seems to be unavailable right now");
                    default:
                        return Result.Fail<List<ResultItem>>(ex.Message ?? "Something went wrong");
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<List<ResultItem>>($"Check your internet connection {ex}");
            }
        }

        public async Task<Result<PokemonSpecie>> GetPokemonSpecieInfo(int pokeId)
        {
            try
            {
                var pokemon = await NetworkHandler.GetData<PokemonSpecie>(Constants.PokemonAPIBaseAddress + "pokemon-species/" + pokeId);
                return Result.Ok(pokemon);
            }
            catch (NetworkErrorException ex)
            {
                // You can customize the error messages just checking the exceptionCode or just use the exceptionMessage instead (see default case)
                switch (ex.Code)
                {
                    case (int)HttpStatusCode.NotFound:
                        return Result.Fail<PokemonSpecie>("Can't retrieve pokemon info at this moment");
                    default:
                        return Result.Fail<PokemonSpecie>(ex.Message ?? "Something went wrong");
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<PokemonSpecie>($"Check your internet connection {ex.ToString()}");
            }
        }

        public async Task<Result<List<ResultItem>>> GetPokemonTypesList()
        {
            try
            {
                var typesList = await NetworkHandler.GetData<BasicListResponse>(Constants.PokemonAPIBaseAddress + "type/");
                return Result.Ok(typesList.Results);
            }
            catch (NetworkErrorException ex)
            {
                // You can customize the error messages just checking the exceptionCode or just use the exceptionMessage instead (see default case)
                switch (ex.Code)
                {
                    case (int)HttpStatusCode.NotFound:
                        return Result.Fail<List<ResultItem>>("Can't retrieve types list at this moment");
                    default:
                        return Result.Fail<List<ResultItem>>(ex.Message ?? "Something went wrong");
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<List<ResultItem>>($"Check your internet connection {ex.ToString()}");
            }
        }

        public async Task<Result<TypeResponse>> GetTypeInfo(int typeId)
        {
            try
            {
                var type = await NetworkHandler.GetData<TypeResponse>(Constants.PokemonAPIBaseAddress + "type/" + typeId);
                return Result.Ok(type);
            }
            catch (NetworkErrorException ex)
            {
                // You can customize the error messages just checking the exceptionCode or just use the exceptionMessage instead (see default case)
                switch (ex.Code)
                {
                    case (int)HttpStatusCode.NotFound:
                        return Result.Fail<TypeResponse>("Can't retrieve type info at this moment");
                    default:
                        return Result.Fail<TypeResponse>(ex.Message ?? "Something went wrong");
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<TypeResponse>($"Check your internet connection {ex.ToString()}");
            }
        }

        public async Task<Result<List<ResultItem>>> GetPokemonGenerationList()
        {
            try
            {
                var generationList = await NetworkHandler.GetData<BasicListResponse>(Constants.PokemonAPIBaseAddress + "generation/");
                return Result.Ok(generationList.Results);
            }
            catch (NetworkErrorException ex)
            {
                // You can customize the error messages just checking the exceptionCode or just use the exceptionMessage instead (see default case)
                switch (ex.Code)
                {
                    case (int)HttpStatusCode.NotFound:
                        return Result.Fail<List<ResultItem>>("Can't retrieve generation list at this moment");
                    default:
                        return Result.Fail<List<ResultItem>>(ex.Message ?? "Something went wrong");
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<List<ResultItem>>($"Check your internet connection {ex.ToString()}");
            }
        }

        public async Task<Result<GenerationResponse>> GetGenerationInfo(int generationId)
        {
            try
            {
                var generation = await NetworkHandler.GetData<GenerationResponse>(Constants.PokemonAPIBaseAddress + "generation/" + generationId);
                return Result.Ok(generation);
            }
            catch (NetworkErrorException ex)
            {
                // You can customize the error messages just checking the exceptionCode or just use the exceptionMessage instead (see default case)
                switch (ex.Code)
                {
                    case (int)HttpStatusCode.NotFound:
                        return Result.Fail<GenerationResponse>("Can't retrieve generation info at this moment");
                    default:
                        return Result.Fail<GenerationResponse>(ex.Message ?? "Something went wrong");
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<GenerationResponse>($"Check your internet connection {ex.ToString()}");
            }
        }

        public async Task<Result<EvolutionChainResponse>> GetEvolutionChainByPokemonId(int id)
        {
            try
            {
                var evolutionChain = await NetworkHandler.GetData<EvolutionChainResponse>(Constants.PokemonAPIBaseAddress + $"evolution-chain/{id}");
                return Result.Ok(evolutionChain);
            }
            catch (NetworkErrorException ex)
            {
                // You can customize the error messages just checking the exceptionCode or just use the exceptionMessage instead (see default case)
                switch (ex.Code)
                {
                    case (int)HttpStatusCode.InternalServerError:
                        return Result.Fail<EvolutionChainResponse>("It seems like PokeApi server is down");
                    case (int)HttpStatusCode.NotFound:
                        return Result.Fail<EvolutionChainResponse>($"The evolution chain \"{id}\" seems to be unavailable right now");
                    default:
                        return Result.Fail<EvolutionChainResponse>(ex.Message ?? "Something went wrong");
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<EvolutionChainResponse>($"Check your internet connection {ex}");
            }
        }
    }
}

