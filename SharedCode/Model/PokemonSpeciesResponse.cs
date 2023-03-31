using System;
using System.Collections.Generic;

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
    }
}

