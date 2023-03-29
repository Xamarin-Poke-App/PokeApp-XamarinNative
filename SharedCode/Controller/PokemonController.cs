using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using SharedCode.Model;
using SharedCode.Repository;
using SharedCode.Services;
using SharedCode.Util;
using Unity;

namespace SharedCode.Controller
{
	public interface IPokemonController
	{
		void updateView(Result<List<ResultPokemons>> data);
	}

	public class PokemonController
	{
		private IPokemonController viewListener;
		private IPokemonRepository Repository = IocContainer.GetInstance().Resolve<IPokemonRepository>();

		public PokemonController(IPokemonController listener)
		{
			this.viewListener = listener;
		}

		public async void GetAllPokemonsSpecies()
		{
			var data = await Repository.GetPokemonList();
			viewListener.updateView(data);
			//try
			//{
   //             var pokemons = await NetworkHandler.GetData<PokemonSpeciesResponse>("pokemon-species");
   //             viewListener.updateView(Result.Ok(pokemons.count));
   //         }
   //         catch (NetworkErrorException ex)
			//{
			//	// You can customize the error messages just checking the exceptionCode or just use the exceptionMessage instead (see default case)
			//	switch (ex.Code)
			//	{
			//		case (int) HttpStatusCode.InternalServerError:
   //                     viewListener.updateView(Result.Fail<int>("It seems like PokeApi server is down"));
   //                     break;
			//		case (int) HttpStatusCode.NotFound:
   //                     viewListener.updateView(Result.Fail<int>("The pokemon list seems to be unavailable right now"));
   //                     break;
			//		default:
			//			viewListener.updateView(Result.Fail<int>(ex.Message ?? "Something went wrong"));
			//			break;
			//	}
			//}
   //         catch (Exception ex)
   //         {
   //             viewListener.updateView(Result.Fail<int>($"Check your internet connection {ex.ToString()}"));
   //         }
        }
	}
}

