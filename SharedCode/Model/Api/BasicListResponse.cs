using System;
using System.Collections.Generic;

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
    }
}

