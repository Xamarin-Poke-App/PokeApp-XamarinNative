using System;
using SharedCode.Model;
using SharedCode.Util;
using System.Net;
using SharedCode.Repository;
using Unity;
using SharedCode.Repository.Interfaces;
using SharedCode.Model.DB;
using SharedCode.Interfaces;

namespace SharedCode.Controller
{
    public interface IPokemonDetailController
    {
        IPokemonDetailControllerListener listener { get; set; }
        void LoadPokemonInfo(int pokeId);
        void GetEvolutionChainByPokemonId(int id);
    }

    public interface IPokemonDetailControllerListener
    {
        void updatePokemonInfo(Result<PokemonLocal> pokemon);
    }

	public class PokemonDetailController : IPokemonDetailController
	{
        public IPokemonDetailControllerListener viewListener;

        [Dependency]
        public IPokemonDetailService Repository;

        public IPokemonDetailControllerListener listener { get => viewListener; set => viewListener = value; }

        public async void LoadPokemonInfo(int pokeId)
        {
            var data = await Repository.UpdateOrGetPokemonByIdLocalAsync(pokeId);
            viewListener.updatePokemonInfo(data);
        }
        
        public async void GetEvolutionChainByPokemonId(int id)
        {
            var data = await Repository.GetEvolutionChainByPokemonId(id);
        }
    }
}

