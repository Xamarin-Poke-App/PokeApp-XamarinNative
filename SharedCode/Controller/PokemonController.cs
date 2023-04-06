using System.Collections.Generic;
using System.Linq;
using System.Timers;
using SharedCode.Interfaces;
using SharedCode.Model;
using SharedCode.Model.Api;
using SharedCode.Model.DB;
using SharedCode.Repository;
using SharedCode.Repository.Interfaces;
using SharedCode.Util;
using Unity;

namespace SharedCode.Controller
{
	public interface IPokemonController
	{
		IPokemonControllerListener listener { get; set; }
		void GetAllPokemonsSpecies();
		void FilterPokemonListByName(string query);
	}

	public interface IPokemonControllerListener
	{
		void updateView(Result<List<PokemonLocal>> data);
    }

	public class PokemonController : IPokemonController
    {
        private List<PokemonLocal> pokemons;
        private List<PokemonLocal> filteredPokemons;

        public IPokemonControllerListener viewListener;
        
        [Dependency]
		public IPokemonService Repository;

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

		public PokemonController()
		{
            pokemons = new List<PokemonLocal>();
            filteredPokemons = new List<PokemonLocal>();
        }

		public async void GetAllPokemonsSpecies()
		{

            var data = await Repository.GetPokemonData();
            viewListener.updateView(data);
            pokemons = data.Value;
            filteredPokemons = data.Value;
        }

        public void FilterPokemonListByName(string query)
        {
            if (query == "")
            {
                filteredPokemons = pokemons;
                viewListener.updateView(Result.Ok(filteredPokemons));
                return;
            }
            string lowerQuery = query.ToLower();
            filteredPokemons = pokemons.Where(p => p.Name.ToLower().Contains(lowerQuery)).ToList();
            viewListener.updateView(Result.Ok(filteredPokemons));
        }
    }
}

