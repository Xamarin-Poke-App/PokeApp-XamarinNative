using System;
using System.Linq;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using AndroidX.RecyclerView.Widget;
using PokeAppAndroid.Adapters;
using PokeAppAndroid.Utils;
using SharedCode.Controller;
using SharedCode.Interfaces;
using SharedCode.Model;
using SharedCode.Model.Api;
using SharedCode.Model.DB;
using SharedCode.Services;
using SharedCode.Util;

namespace PokeAppAndroid.View
{
    public class PokemonEvolutionFragment : AndroidX.Fragment.App.Fragment, IPokemonDetailControllerListener
    {
        private IPokemonDetailController _pokemonDetailController = IocContainer.GetDependency<IPokemonDetailController>();

        private int _pokemonId;
        private PokemonLocal _pokemon;
        private EvolutionChainResponse _evolutionChain;
        
        private RecyclerView RvEvolutionChain;

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_evolution_detail, container, false);
            RvEvolutionChain = view.FindViewById<RecyclerView>(Resource.Id.RvEvolutionChain);
            _pokemonDetailController.listener = this;
            LoadArgs();
            return view;
        }

        public void LoadArgs()
        {
            if (Arguments == null) return;
            _pokemonId = Arguments.GetInt(Constants.PokemonIdArg);

            if (_pokemonId == 0) return;
            _pokemonDetailController.LoadPokemonInfo(_pokemonId);
        }

        public void updatePokemonImage(Result<byte[]> image)
        {
        }

        public void updatePokemonInfo(Result<PokemonLocal> pokemon)
        {
            if (pokemon.IsFailure) return;
            _pokemon = pokemon.Value;
            _pokemonDetailController.GetEvolutionChainByPokemonId(_pokemon.EvolutionChainId);
        }

        public void updateEvoutionChain(Result<EvolutionChainResponse> evolutionChain)
        {
            if (evolutionChain.IsFailure) return;
            _evolutionChain = evolutionChain.Value;

            var mLayoutManager = new LinearLayoutManager(RequireContext());
            RvEvolutionChain.SetLayoutManager(mLayoutManager);

            //if (viewHolder.RvTypes.ItemDecorationCount < 1)
            //    viewHolder.RvTypes.AddItemDecoration(new SpaceItemDecorator(15, 0));

            var primaryType = _pokemon.TypesArray.FirstOrDefault();
            var color = ViewExtensions.GetColorForType(RequireContext(), primaryType);

            var adapter = new EvolutionChainAdapter(_evolutionChain.GetListOfChains(), color);
            RvEvolutionChain.SetAdapter(adapter);
        }
    }
}

