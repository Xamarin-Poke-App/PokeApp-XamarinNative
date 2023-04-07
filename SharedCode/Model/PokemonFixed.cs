using System;
using System.Collections.Generic;
using SharedCode.Util;

namespace SharedCode.Model
{
	public class PokemonFixed
	{
        public string ID;
        public string Name;
        public string ImageURL;
        public List<Enums.PokemonTypes> Types;

        public PokemonFixed(string id, string name, string image, List<Enums.PokemonTypes> types)
        {
            this.ID = id;
            this.Name = name;
            this.ImageURL = image;
            this.Types = types;
        }
    }
}

