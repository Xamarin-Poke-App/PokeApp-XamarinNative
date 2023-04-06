using System;
using Newtonsoft.Json;
using SharedCode.Model.Api;

namespace SharedCode.Model
{
    public class EvolutionChainResponse
    {
        public Chain chain { get; set; }
        public int id { get; set; }
    }

    public class Chain
    {
        public Chain[] evolves_to { get; set; }
        public ResultItem species { get; set; }
    }
}
