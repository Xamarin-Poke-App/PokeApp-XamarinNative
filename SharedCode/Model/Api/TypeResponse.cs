using System;
using System.Collections.Generic;

namespace SharedCode.Model.Api
{
    public class DoubleDamageFrom
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class GameIndex
    {
        public int game_index { get; set; }
        public Generation generation { get; set; }
    }

    public class Generation
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Language
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Move
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Name
    {
        public Language language { get; set; }
        public string name { get; set; }
    }

    public class NoDamageFrom
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class NoDamageTo
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Pokemon
    {
        public ResultItem pokemon { get; set; }
        public int slot { get; set; }
    }

    public class TypeResponse
    {
        public Generation generation { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public List<Pokemon> pokemon { get; set; }
    }

}

