using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SharedCode.Model;
using SharedCode.Util;

namespace SharedCode.Repository
{
	interface IPokemonRepository
	{
        Task<Result<List<ResultPokemons>>> GetPokemonList();
	}

	public class PokemonRepository : IPokemonRepository
	{
        private readonly INetworkHandler NetworkHandler;

		public PokemonRepository(INetworkHandler networkHandler)
		{
            NetworkHandler = networkHandler;
		}

        public async Task<Result<List<ResultPokemons>>> GetPokemonList()
        {
            try
            {
                var pokemons = await NetworkHandler.GetData<PokemonSpeciesResponse>("pokemon-species?limit=10000");
                return Result.Ok(pokemons.results);
            }
            catch (NetworkErrorException ex)
            {
                // You can customize the error messages just checking the exceptionCode or just use the exceptionMessage instead (see default case)
                switch (ex.Code)
                {
                    case (int)HttpStatusCode.InternalServerError:
                        return Result.Fail<List<ResultPokemons>>("It seems like PokeApi server is down");
                    case (int)HttpStatusCode.NotFound:
                        return Result.Fail<List<ResultPokemons>>("The pokemon list seems to be unavailable right now");
                    default:
                        return Result.Fail<List<ResultPokemons>>(ex.Message ?? "Something went wrong");
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<List<ResultPokemons>>($"Check your internet connection {ex}");
            }
        }
    }
}

