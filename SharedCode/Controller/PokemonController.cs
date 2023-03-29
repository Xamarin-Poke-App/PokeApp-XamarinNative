using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using SharedCode.Model;
using SharedCode.Util;

namespace SharedCode.Controller
{
	public interface IPokemonController
	{
		void updateView(Result<List<ResultPokemons>> data);
	}

	public class PokemonController
	{
		private IPokemonController viewListener;

		public PokemonController(IPokemonController listener)
		{
			this.viewListener = listener;
		}

		public async void GetAllPokemonsSpecies()
		{
			try
			{
                var pokemons = await NetworkHandler.GetData<PokemonSpeciesResponse>("pokemon-species");
                viewListener.updateView(Result.Ok(pokemons.results));
            }
            catch (NetworkErrorException ex)
			{
				// You can customize the error messages just checking the exceptionCode or just use the exceptionMessage instead (see default case)
				switch (ex.Code)
				{
					case (int) HttpStatusCode.InternalServerError:
                        viewListener.updateView(Result.Fail<List<ResultPokemons>>("It seems like PokeApi server is down"));
                        break;
					case (int) HttpStatusCode.NotFound:
                        viewListener.updateView(Result.Fail<List<ResultPokemons>>("The pokemon list seems to be unavailable right now"));
                        break;
					default:
						viewListener.updateView(Result.Fail<List<ResultPokemons>>(ex.Message ?? "Something went wrong"));
						break;
				}
			}
            catch (Exception ex)
            {
                viewListener.updateView(Result.Fail<List<ResultPokemons>>($"Check your internet connection {ex.ToString()}"));
            }
        }
	}
}

