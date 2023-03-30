using System;
using SharedCode.Model;
using SharedCode.Util;
using System.Net;

namespace SharedCode.Controller
{
    public interface IPokemonDetailControllerListener
    {
        void updatePokemonImage(Result<byte[]> image);
        void updatePokemonInfo(Result<PokemonSpecie> pokemon);
    }

	public class PokemonDetailController
	{
        public IPokemonDetailControllerListener viewListener;

		public PokemonDetailController(IPokemonDetailControllerListener listener)
		{
            this.viewListener = listener;
		}

        public async void GetPokemonInfo(int pokeId)
        {
            try
            {
                var pokemon = await NetworkHandler.GetData<PokemonSpecie>(NetworkHandler.BaseAddress + "pokemon-species/" + pokeId);
                viewListener.updatePokemonInfo(Result.Ok(pokemon));
                var data = await NetworkHandler.LoadImage("https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/" + pokemon.id + ".png");
                viewListener.updatePokemonImage(Result.Ok<byte[]>(data));
            }
            catch (NetworkErrorException ex)
            {
                // You can customize the error messages just checking the exceptionCode or just use the exceptionMessage instead (see default case)
                switch (ex.Code)
                {
                    case (int)HttpStatusCode.NotFound:
                        viewListener.updatePokemonInfo(Result.Fail<PokemonSpecie>("Can't retrieve pokemon info at this moment"));
                        break;
                    default:
                        viewListener.updatePokemonInfo(Result.Fail<PokemonSpecie>(ex.Message ?? "Something went wrong"));
                        break;
                }
            }
            catch (Exception ex)
            {
                viewListener.updatePokemonInfo(Result.Fail<PokemonSpecie>($"Check your internet connection {ex.ToString()}"));
            }
        }
    }
}

