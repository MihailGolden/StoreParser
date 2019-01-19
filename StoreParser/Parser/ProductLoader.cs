using StoreParser.Parser.ProDjShopUrlCollector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreParser.Parser
{
    public class ProductLoader
    {
        readonly HttpClient client;

        public ProductLoader()
        {
            client = new HttpClient();
        }

        public async Task<string> Load(string url)
        {
            var response = await client.GetAsync(url);
            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
