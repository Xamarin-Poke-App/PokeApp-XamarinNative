using System;
using System.Collections.Generic;
using SharedCode.Util;

namespace SharedCode.Model.Api
{
    public class BasicListResponse
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<ResultItem> results { get; set; }
    }

    public class ResultItem
    {
        public string name { get; set; }
        public string url { get; set; }

        public int GetIdFromUrl()
        {
            var auxUrl = url.Remove(url.Length - 1);
            var index = auxUrl.LastIndexOf("/");
            int id;
            if (Int32.TryParse(auxUrl.Substring(index + 1), out id))
            {
                return id;
            }
            return -1;
        }
        
        public string FormatedName()
        {
            var list = name.Split('-');
            string newName = "";
            for (var i = 0; i < list.Length; i++)
            {
                var str = list[i];
                newName += (char.ToUpper(str[0]) + str.Substring(1)) + " ";
            }

            return newName.Trim();
        }

        public string GetPokemonImageURL(string id)
        {
            return $"{Constants.PokemonArtWorksImagesBaseAddress}{id}.png";
        }

        public PokemonFixed ToPokemonFixed()
        {
            string ID = GetIdFromUrl().ToString();
            string imageURL = GetPokemonImageURL(ID);
            string name = this.FormatedName();
            var list = getRandomTypes();
            return new PokemonFixed(ID, name, imageURL, list);
        }

        private List<Enums.PokemonTypes> getRandomTypes()
        {
            var list = new List<Enums.PokemonTypes>();
            Random random = new Random();
            Array values = Enum.GetValues(typeof(Enums.PokemonTypes));

            if (random.Next() % 2 == 0)
            {
                list.Add((Enums.PokemonTypes)values.GetValue(random.Next(values.Length)));
            }
            list.Add((Enums.PokemonTypes)values.GetValue(random.Next(values.Length)));

            return list;
        }
    }
}

