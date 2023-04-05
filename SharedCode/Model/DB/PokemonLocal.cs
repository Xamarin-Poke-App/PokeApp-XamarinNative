using System;
using System.Collections.Generic;
using SQLite;

namespace SharedCode.Model.DB
{
    [Table("Pokemon")]
    public class PokemonLocal
	{
		public string Name { get; set; }

		[PrimaryKey]
		public int Id { get; set; }
		public string Region { get; set; }
		public string Types { get; set; }
		public string RegularSpriteUrl { get; set; }

		public PokemonLocal() { }

		public PokemonLocal(string name, int id)
		{
			Name = name;
			Id = id;
			Types = "";
		}
	}
}

