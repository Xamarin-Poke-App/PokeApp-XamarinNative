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
    }
}

