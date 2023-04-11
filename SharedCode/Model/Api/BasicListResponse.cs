using System;
using System.Collections.Generic;
using SharedCode.Util;
using System.Text.Json.Serialization;

namespace SharedCode.Model.Api
{
    public class BasicListResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("previous")]
        public string Previous { get; set; }

        [JsonPropertyName("results")]
        public List<ResultItem> Results { get; set; }
    }

    public class ResultItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        public int GetIdFromUrl()
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
        
        public string FormatedName()
        {
            var list = Name.Split('-');
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

        public string GetPokemonShinyImageURL(string id)
        {
            return $"{Constants.PokemonShinyArtWorksImagesBaseAddress}{id}.png";
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

