using System.Collections.Generic;
using SharedCode.Model;
using SharedCode.Repository;
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

		public async void GetAllPokemonsSpecies()
		{
			var data = await Repository.GetPokemonList();
			viewListener.updateView(data);
        }
	}
}

