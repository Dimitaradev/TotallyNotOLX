using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
namespace TotallyNotOLX.StaticHelpers
{
    public static class SearchesLogger
    {
        public static List<SearchCountPair> Searches { get; set; } = GetSearches();

        public static void Log(string searchName)
        {
            string search = searchName.ToLower();
            if (Searches.Where(x=>x.SearchName == search).Any())
            {
                Searches.Where(x => x.SearchName == search)
                    .FirstOrDefault()
                    .SearchCount++;
            }
            else
            {
                Searches.Add(new SearchCountPair() {
                        SearchName=search,
                        SearchCount=1
                    }
                );
            }
            string json = System.Text.Json.JsonSerializer.Serialize(Searches);
            File.WriteAllText(@$"./wwwroot/searches.json", json);
        }

        public static List<string> GetTopSearches(int topCount)
        {
            return Searches.OrderByDescending(x => x.SearchCount)
                .Take(topCount)
                .Select(x=>x.SearchName)
                .ToList();
        }

        private static List<SearchCountPair> GetSearches()
        {
            using (StreamReader r = new StreamReader(@"./wwwroot/searches.json"))
            {
                string json = r.ReadToEnd();
                if (string.IsNullOrEmpty(json))
                {
                    return new List<SearchCountPair>();
                }
                return JsonConvert.DeserializeObject<List<SearchCountPair>>(json);
            }
        }
    }
    public class SearchCountPair
    {
        public string SearchName { get; set; }
        public int SearchCount { get; set; }
    }
}
