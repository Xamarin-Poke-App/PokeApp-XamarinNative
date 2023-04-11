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
        void LoadPokemonInfo(int pokeId);
        void GetEvolutionChainByPokemonId(int id);
    }

    public interface IPokemonDetailControllerListener
    {
        void updatePokemonInfo(Result<PokemonLocal> pokemon);
        void updateEvoutionChain(Result<EvolutionChainResponse> evolutionChain);
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
            var data = await Repository.UpdateOrGetEvolutionChainByPokemonId(id);
            viewListener.updateEvoutionChain(data);
        }
    }
}

