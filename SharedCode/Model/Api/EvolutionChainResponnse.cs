using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using SharedCode.Model.Api;

namespace SharedCode.Model.Api
{
    public class EvolutionChainResponse
    {
        [JsonPropertyName("chain")]
        public Chain Chain { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        public List<List<ResultItem>> GetListOfChains()
        {
            var result = new List<List<List<ResultItem>>>();
            return Chain.GetChainAsPairs();
        }
    }

    public class Chain
    {
        [JsonPropertyName("evolves_to")]
        public Chain[] EvolvesTo { get; set; }

        [JsonPropertyName("species")]
        public ResultItem Species { get; set; }

        public List<List<ResultItem>> GetChainAsPairs()
        {
            var chainAsPairs = new List<List<ResultItem>>();
            foreach (var chain in EvolvesTo)
            {
                var firstPair = new List<ResultItem>();
                firstPair.Add(Species);
                firstPair.Add(chain.Species);
                chainAsPairs.Add(firstPair);
                chainAsPairs.AddRange(chain.GetChainAsPairs());
            }

            return chainAsPairs;
        }
    }
}
