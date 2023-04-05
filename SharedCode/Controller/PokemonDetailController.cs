using System;
using SharedCode.Model;
using SharedCode.Util;
using System.Net;
using SharedCode.Repository;
using Unity;

namespace SharedCode.Controller
{
    public interface IPokemonDetailController
    {
        IPokemonDetailControllerListener listener { get; set; }
        void GetPokemonInfo(int pokeId);
        void GetPokemonSpecieInfo(int pokeId);
        void LoadPokemonImage(int pokeId);
    }

    public interface IPokemonDetailControllerListener
    {
        void updatePokemonImage(Result<byte[]> image);
        void updatePokemonSpecieInfo(Result<PokemonSpecie> pokemon);
        void updatePokemonInfo(Result<PokemonInfo> pokemon);
    }

	public class PokemonDetailController : IPokemonDetailController
	{
        public IPokemonDetailControllerListener viewListener;

        [Dependency]
        public IPokemonRepository Repository;

        public IPokemonDetailControllerListener listener { get => viewListener; set => viewListener = value; }

        public async void GetPokemonSpecieInfo(int pokeId)
        {
            var data = await Repository.GetPokemonSpecieInfo(pokeId);
            viewListener.updatePokemonSpecieInfo(data);
        }

        public async void GetPokemonInfo(int pokeId)
        {
            var data = await Repository.GetPokemonInfo(pokeId);
            viewListener.updatePokemonInfo(data);
        }

        public async void LoadPokemonImage(int pokeId)
        {
            var image = await Repository.GetPokemonImage(pokeId);
            viewListener.updatePokemonImage(image);
        }
    }
}

