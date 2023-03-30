using System;
using System.Collections.Generic;
using System.Linq;
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
		void FilterPokemonListByName(string query);
	}

	public interface IPokemonControllerListener
	{
		void updateView(Result<List<ResultPokemons>> data);
    }

	public class PokemonController : IPokemonController
    {
        private List<ResultPokemons> pokemons;
        private List<ResultPokemons> filteredPokemons;

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

		public PokemonController()
		{
            pokemons = new List<ResultPokemons>();
            filteredPokemons = new List<ResultPokemons>();
        }

		public async void GetAllPokemonsSpecies()
		{
			var data = await Repository.GetPokemonList();
            viewListener.updateView(data);
			if (data.IsFailure) return;
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
            filteredPokemons = pokemons.Where(p => p.name.ToLower().Contains(lowerQuery)).ToList();
            viewListener.updateView(Result.Ok(filteredPokemons));
        }
    }
}

