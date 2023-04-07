using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SharedCode.Model.Api
{
    public class DoubleDamageFrom
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class GameIndex
    {
        [JsonPropertyName("game_index")]
        public int GameIndexNumber { get; set; }

        [JsonPropertyName("generation")]
        public Generation Generation { get; set; }
    }

    public class Generation
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Language
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Move
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Name
    {
        [JsonPropertyName("language")]
        public Language Language { get; set; }

        [JsonPropertyName("name")]
        public string NameText { get; set; }
    }

    public class NoDamageFrom
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class NoDamageTo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Pokemon
    {
        [JsonPropertyName("pokemon")]
        public ResultItem PokemonItem { get; set; }

        [JsonPropertyName("slot")]
        public int Slot { get; set; }
    }

    public class TypeResponse
    {
        [JsonPropertyName("generation")]
        public Generation Generation { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("pokemon")]
        public List<Pokemon> Pokemon { get; set; }
    }

}

