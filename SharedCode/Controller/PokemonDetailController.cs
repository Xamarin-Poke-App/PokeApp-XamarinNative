using System;
using SharedCode.Model;
using SharedCode.Util;
using System.Net;
using SharedCode.Repository;
using Unity;
using SharedCode.Repository.Interfaces;
using SharedCode.Model.DB;

namespace SharedCode.Controller
{
    public interface IPokemonDetailController
    {
        IPokemonDetailControllerListener listener { get; set; }
        void LoadPokemonImage(int pokeId);
    }

    public interface IPokemonDetailControllerListener
    {
        void updatePokemonImage(Result<byte[]> image);
        void updatePokemonInfo(Result<PokemonLocal> pokemon);
    }

	public class PokemonDetailController : IPokemonDetailController
	{
        public IPokemonDetailControllerListener viewListener;

        [Dependency]
        public IPokemonRepository Repository;

        public IPokemonDetailControllerListener listener { get => viewListener; set => viewListener = value; }

        public async void LoadPokemonImage(int pokeId)
        {
            var image = await Repository.GetPokemonImage(pokeId);
            viewListener.updatePokemonImage(image);
        }
    }
}

