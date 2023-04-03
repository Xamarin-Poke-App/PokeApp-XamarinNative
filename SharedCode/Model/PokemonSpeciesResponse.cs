using System;
using System.Collections.Generic;
using SharedCode.Util;

namespace SharedCode.Model
{
    public class PokemonSpeciesResponse
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<ResultPokemons> results { get; set; }
    }

    public class ResultPokemons
    {
        public string name { get; set; }
        public string url { get; set; }

        public int GetIdFromUrl()
        {
            var auxUrl = url.Remove(url.Length - 1);
            var index = auxUrl.LastIndexOf("/");
            int id;
            if (Int32.TryParse(auxUrl.Substring(index + 1), out id))
            {
                return id;
            }
            return -1;
        }
        
        public string FormatedName()
        {
            var list = name.Split('-');
            string newName = "";
            for (var i = 0; i < list.Length; i++)
            {
                var str = list[i];
                newName += (char.ToUpper(str[0]) + str.Substring(1)) + " ";
            }

            return newName.Trim();
        }

        public string GetPokemonImageURL(string id)
        {
            return $"{Constants.PokemonArtWorksImagesBaseAddress}{id}.png";
        }
    }
}

