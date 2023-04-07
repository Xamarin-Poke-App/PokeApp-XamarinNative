using System;
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
    }

    public class Chain
    {
        [JsonPropertyName("evolves_to")]
        public Chain[] EvolvesTo { get; set; }

        [JsonPropertyName("species")]
        public ResultItem Species { get; set; }
    }
}
