using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SharedCode.Model.Api
{
    public class MainRegion
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class GenerationResponse
    {
        [JsonPropertyName("abilities")]
        public List<object> Abilities { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("main_region")]
        public MainRegion MainRegion { get; set; }

        [JsonPropertyName("moves")]
        public List<Move> Moves { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("pokemon_species")]
        public List<ResultItem> PokemonSpecies { get; set; }

        [JsonPropertyName("types")]
        public List<Type> Types { get; set; }
    }

    public class Type
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}

