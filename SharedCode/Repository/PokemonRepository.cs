using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SharedCode.Model;
using SharedCode.Util;

namespace SharedCode.Repository
{
	public interface IPokemonRepository
	{
        Task<Result<List<ResultPokemons>>> GetPokemonList();
        Task<Result<PokemonSpecie>> GetPokemonInfo(int pokeId);
        Task<Result<byte[]>> GetPokemonImage(int pokeId);
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
                var pokemons = await NetworkHandler.GetData<PokemonSpeciesResponse>(Constants.PokemonAPIBaseAddress + "pokemon-species?limit=10000");
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

        public async Task<Result<PokemonSpecie>> GetPokemonInfo(int pokeId)
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

        public async Task<Result<byte[]>> GetPokemonImage(int pokeId)
        {
            try
            {
                var data = await NetworkHandler.LoadImage(Constants.PokemonArtWorksImagesBaseAddress + pokeId + ".png");
                return Result.Ok<byte[]>(data);
            }
            catch (NetworkErrorException ex)
            {
                // You can customize the error messages just checking the exceptionCode or just use the exceptionMessage instead (see default case)
                switch (ex.Code)
                {
                    case (int)HttpStatusCode.NotFound:
                        return Result.Fail<byte []>("Can't retrieve pokemon image");
                    default:
                        return Result.Fail<byte[]>(ex.Message ?? "Something went wrong");
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<byte[]>($"Check your internet connection {ex.ToString()}");
            }
        }
    }
}

