using System;
using System.Collections.Generic;
using Java.Net;
using SharedCode.Util;
using SQLite;

namespace SharedCode.Model.DB
{
    [Table("Pokemon")]
    public class PokemonLocal : IGenericId
	{
		public string Name { get; set; }

		[PrimaryKey]
		public int Id { get; set; }
		public string Region { get; set; }
		public string Types { get; set; }
		public string RegularSpriteUrl { get; set; }
		public string ShinySprite { get; set; }
        public int BaseHappiness { get; set; }
		public string Generation { get; set; }
		public string Habitat { get; set; }
		public int EvolutionChainId { get; set; }
		public string FlavorTextEntry { get; set; }

        public PokemonLocal() { }

		public PokemonLocal(string name, int id)
		{
			Name = name;
			Id = id;
			Types = "";
			RegularSpriteUrl = Constants.PokemonArtWorksImagesBaseAddress + $"{id}.png";
			ShinySprite = Constants.PokemonShinyArtWorksImagesBaseAddress + $"{id}.png";
        }

		public string[] TypesArray
		{
			get
			{
				return Types.Split('%');
            }
		}

		public string GetGenerationRomanNumeral()
		{
			if (Generation == null) return "";
            var index = Generation.LastIndexOf("-");
            return Generation.Substring(index + 1).ToUpper();
		}
	}
}

