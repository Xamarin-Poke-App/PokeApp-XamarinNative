using System;
using System.Net;
using System.Net.Http;
using SharedCode.Model;
using SharedCode.Util;

namespace SharedCode.Controller
{
	public interface IPokemonController
	{
		void updateView(int? pokeCount, string errorMsg);
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
                Console.WriteLine("Finish");
                Console.WriteLine(pokemons.count);
                viewListener.updateView(pokemons.count, null);
            } catch (NetworkErrorException ex)
			{
				Console.WriteLine("ERROR");
				switch (ex.Code)
				{
					case (int) HttpStatusCode.BadRequest:
						viewListener.updateView(null, "Bad request");
						break;
					case (int) HttpStatusCode.InternalServerError:
                        viewListener.updateView(null, "It seems like PokeApi server is down");
                        break;
					case (int) HttpStatusCode.NotFound:
                        viewListener.updateView(null, "The pokemon list seems to be unavailable right now");
                        break;
					default:
						viewListener.updateView(null, "Check your internet connection");
						break;
				}
			} catch (Exception ex)
			{
				Console.WriteLine("Exception error!!!!");
			}
		}
	}
}

