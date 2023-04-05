using System;
using System.Collections.Generic;

namespace SharedCode.Model.Api
{
    public class MainRegion
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class GenerationResponse
    {
        public List<object> abilities { get; set; }
        public int id { get; set; }
        public MainRegion main_region { get; set; }
        public List<Move> moves { get; set; }
        public string name { get; set; }
        public List<ResultItem> pokemon_species { get; set; }
        public List<Type> types { get; set; }
    }

    public class Type
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}

