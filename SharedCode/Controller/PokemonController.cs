using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SharedCode.Model;
using SharedCode.Repository;
using SharedCode.Services;
using SharedCode.Util;
using Unity;

namespace SharedCode.Controller
{
	public interface IPokemonController
	{
		IPokemonControllerListener listener { get; set; }
		void GetAllPokemonsSpecies();
	}

	public interface IPokemonControllerListener
	{
		void updateView(Result<List<ResultPokemons>> data);
	}

	public class PokemonController : IPokemonController
    {
		public IPokemonControllerListener viewListener;

		[Dependency]
		public IPokemonRepository Repository;

		public IPokemonControllerListener listener
		{
			get
			{
				return viewListener;
			}
			set
			{
				viewListener = value;
			}
		}

		//public PokemonController(IPokemonControllerListener listener)
		//{
		//	this.viewListener = listener;
		//}

		public async void GetAllPokemonsSpecies()
		{
			var data = await Repository.GetPokemonList();
			viewListener.updateView(data);
        }
	}
}

