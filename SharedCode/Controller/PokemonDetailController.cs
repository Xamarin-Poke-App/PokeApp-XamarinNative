using SharedCode.Util;
using Unity;
using SharedCode.Model.DB;
using SharedCode.Model.Api;
using SharedCode.Interfaces;
using System.Threading.Tasks;

namespace SharedCode.Controller
{
    public interface IPokemonDetailController
    {
        IPokemonDetailControllerListener listener { get; set; }
        void LoadPokemonImage(int pokeId);
        void LoadPokemonInfo(int pokeId);
        void GetEvolutionChainByPokemonId(int id);
        Task<Result<byte[]>> LoadPokemonImageAsync(int pokeId);
        Task<Result<byte[]>> LoadPokemonShinyImageAsync(int pokeId);
    }

    public interface IPokemonDetailControllerListener
    {
        void updatePokemonImage(Result<byte[]> image);
        void updatePokemonInfo(Result<PokemonLocal> pokemon);
        void updateEvoutionChain(Result<EvolutionChainResponse> evolutionChain);
    }

	public class PokemonDetailController : IPokemonDetailController
	{
        public IPokemonDetailControllerListener viewListener;

        [Dependency]
        public IPokemonDetailService Repository;

        public IPokemonDetailControllerListener listener { get => viewListener; set => viewListener = value; }

        public async void LoadPokemonImage(int pokeId)
        {
            var image = await Repository.GetPokemonImage(pokeId);
            viewListener.updatePokemonImage(image);
        }

        public async void LoadPokemonInfo(int pokeId)
        {
            var data = await Repository.UpdateOrGetPokemonByIdLocalAsync(pokeId);
            viewListener.updatePokemonInfo(data);
        }
        
        public async void GetEvolutionChainByPokemonId(int id)
        {
            var data = await Repository.UpdateOrGetEvolutionChainByPokemonId(id);
            viewListener.updateEvoutionChain(data);
        }

        public async Task<Result<byte[]>> LoadPokemonImageAsync(int pokeId)
        {
            return await Repository.GetPokemonImage(pokeId);
        }


        public async Task<Result<byte[]>> LoadPokemonShinyImageAsync(int pokeId)
        {
            return await Repository.GetPokemonShinyImage(pokeId);
        }
    }
}

