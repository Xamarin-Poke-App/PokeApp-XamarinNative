using System;
using SQLite;
using SharedCode.Util;

namespace SharedCode.Model.Local
{
    [Table(name: Constants.EvolutionChainTable)]
    public class EvolutionChainLocal
	{
        [PrimaryKey]
        public int Id { get; set; }
        public string Chain { get; set; }
	}
}
