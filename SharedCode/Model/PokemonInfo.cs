using System;
using System.Collections.Generic;

namespace SharedCode.Model
{
	public class PokemonInfo
	{
		public List<Abilities> abilities { get; set; }
        public int base_experience { get; set; }
        public List<Forms> forms { get; set; }
        public List<GameIndices> game_indices { get; set; }
        public int height { get; set; }
        public List<HeldItems> held_items { get; set; }
        public int id { get; set; }
        public bool is_default { get; set; }
        public string location_area_encounters { get; set; }
        public List<Moves> moves { get; set; }
        public string name { get; set; }
        public int order { get; set; }
        public List<PastTypes> past_types { get; set; }
        public Species species { get; set; }
        //public List<Sprites> sprites { get; set; }
        public List<Stats> stats { get; set; }
        public List<Types> types { get; set; }
        public int weight { get; set; }
    }

	public class Abilities
	{
        public Ability ability { get; set; }
        public bool is_hidden { get; set; }
		public int slot { get; set; }
    }

	public class Ability
	{
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Forms
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class GameIndices
    {
        public int game_index { get; set; }
        public Version version { get; set; }
    }

    public class HeldItems
    {
        public Item item { get; set; }
        public List<VersionDetails> version_details { get; set; }
    }

    public class Item
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class VersionDetails
    {
        public int rarity { get; set; }
        public Version version { get; set; }
    }

    public class Moves
    {
        public Move move { get; set; }
        public VersionGroupDetils[] version_group_detail { get; set; }
}

    public class Move
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class PastTypes
    {
        public Generation generation { get; set; }
        public List<Types> types { get; set; }
    }

    public class VersionGroupDetils
    {
        public int level_learned_at { get; set; }
        public MoveLearnMethod move_learn_method { get; set; }
        public VersionGroup version_group { get; set; }
    }

    public class MoveLearnMethod
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class VersionGroup
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Species
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Sprites
    {
        public string back_default { get; set; }
        public string back_female { get; set; }
        public string back_shiny { get; set; }
        public string back_shiny_female { get; set; }
        public string front_default { get; set; }
        public string front_female { get; set; }
        public string front_shiny { get; set; }
        public string front_shiny_female { get; set; }
        public Other other { get; set; }
    }

    public class Other
    {
        public DreamWorld dream_world { get; set; }
        public Home home { get; set; }
        public OfficialArtwork official_artwork { get; set; }
}

    public class DreamWorld
    {
        public string front_default { get; set; }
        public string front_female { get; set; }
    }

    public class Home
    {
        public string back_default { get; set; }
        public string back_female { get; set; }
        public string back_shiny { get; set; }
        public string back_shiny_female { get; set; }
    }

    public class OfficialArtwork
    {
        public string front_default { get; set; }
        public string front_shiny { get; set; }
    }

    public class Stats
    {
        public int base_stat { get; set; }
        public int effort { get; set; }
        public Stat stat { get; set; }
    }

    public class Stat
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Types
    {
        public int slot { get; set; }
        public Type type { get; set; }
    }

    public class Type
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}

