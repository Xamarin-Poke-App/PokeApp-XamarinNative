﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SharedCode.Model.Api;

namespace SharedCode.Model
{
    public class Color
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class EggGroup
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class EvolutionChain
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        public int GetEvolutionChainIdFromUrl()
        {
            var auxUrl = Url.Remove(Url.Length - 1);
            var index = auxUrl.LastIndexOf("/");
            int id;
            if (Int32.TryParse(auxUrl.Substring(index + 1), out id))
            {
                return id;
            }
            return -1;
        }
    }

    public class EvolvesFromSpecies
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class FlavorTextEntry
    {
        [JsonPropertyName("flavor_text")]
        public string FlavorText { get; set; }

        [JsonPropertyName("language")]
        public Language Language { get; set; }

        [JsonPropertyName("version")]
        public Version Version { get; set; }
    }

    public class FormDescription
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("language")]
        public Language Language { get; set; }
    }

    public class Genera
    {
        [JsonPropertyName("genus")]
        public string Genus { get; set; }

        [JsonPropertyName("language")]
        public Language Language { get; set; }
    }

    public class Generation
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class GrowthRate
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

    public class Name
    {
        [JsonPropertyName("language")]
        public Language Language { get; set; }

        [JsonPropertyName("name")]
        public string NameString { get; set; }
    }

    public class Pokedex
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class PokedexNumber
    {
        [JsonPropertyName("entry_number")]
        public int EntryNumber { get; set; }

        [JsonPropertyName("pokedex")]
        public Pokedex Pokedex { get; set; }
    }

    public class PokemonSpecie
    {
        [JsonPropertyName("base_happiness")]
        public int BaseHappiness { get; set; }

        [JsonPropertyName("capture_rate")]
        public int CaptureRate { get; set; }

        [JsonPropertyName("color")]
        public Color Color { get; set; }

        [JsonPropertyName("egg_groups")]
        public List<EggGroup> EggGroups { get; set; }

        [JsonPropertyName("evolution_chain")]
        public EvolutionChain EvolutionChain { get; set; }

        [JsonPropertyName("evolves_from_species")]
        public EvolvesFromSpecies EvolvesFromSpecies { get; set; }

        [JsonPropertyName("flavor_text_entries")]
        public List<FlavorTextEntry> FlavorTextEntries { get; set; }

        [JsonPropertyName("form_descriptions")]
        public List<FormDescription> FormDescriptions { get; set; }

        [JsonPropertyName("forms_switchable")]
        public bool FormsSwitchable { get; set; }

        [JsonPropertyName("gender_rate")]
        public int GenderRate { get; set; }

        [JsonPropertyName("genera")]
        public List<Genera> Genera { get; set; }

        [JsonPropertyName("generation")]
        public Generation Generation { get; set; }

        [JsonPropertyName("habitat")]
        public ResultItem Habitat { get; set; }

        [JsonPropertyName("growth_rate")]
        public GrowthRate GrowthRate { get; set; }

        [JsonPropertyName("has_gender_differences")]
        public bool HasGenderDifferences { get; set; }

        [JsonPropertyName("hatch_counter")]
        public int HatchCounter { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("is_baby")]
        public bool IsBaby { get; set; }

        [JsonPropertyName("is_legendary")]
        public bool IsLegendary { get; set; }

        [JsonPropertyName("is_mythical")]
        public bool IsMythical { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("names")]
        public List<Name> Names { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("pokedex_numbers")]
        public List<PokedexNumber> PokedexNumbers { get; set; }

        [JsonPropertyName("shape")]
        public Shape Shape { get; set; }
    }

    public class Shape
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Version
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}

