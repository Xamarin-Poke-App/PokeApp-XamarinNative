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
        void LoadPokemonInfo(int pokeId);
        void GetEvolutionChainByPokemonId(int id);
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
        [Dependency]
        public IPokemonRepositoryLocal LocalRepository;

        public IPokemonDetailControllerListener listener { get => viewListener; set => viewListener = value; }

        public async void LoadPokemonImage(int pokeId)
        {
            var image = await Repository.GetPokemonImage(pokeId);
            viewListener.updatePokemonImage(image);
        }

        public async void LoadPokemonInfo(int pokeId)
        {
            var data = await LocalRepository.GetPokemonByIdLocalAsync(pokeId);
            viewListener.updatePokemonInfo(data);
        }
        
        public async void GetEvolutionChainByPokemonId(int id)
        {
            var data = await Repository.GetEvolutionChainByPokemonId(id);
        }
    }
}

