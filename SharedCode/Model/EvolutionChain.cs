using System;
using Newtonsoft.Json;

namespace SharedCode.Model
{
    public class EvolutionChainResponse
    {
        public Chain chain { get; set; }
        public int id { get; set; }
    }

    public partial class Chain
    {
        public Chain[] evolves_to { get; set; }
        public ResultPokemons species { get; set; }
    }
}

