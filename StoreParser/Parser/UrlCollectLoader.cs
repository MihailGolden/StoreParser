﻿using StoreParser.Parser.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreParser.Parser
{
    public class UrlCollectLoader
    {
        readonly HttpClient client;
        readonly string url;

        public UrlCollectLoader(IUrlCollectorSettings settings)
        {
            client = new HttpClient();
            url = $"{settings.BaseUrl}/{settings.Prefix}";
        }

        public async Task<string> GetSourceByPageIdAsync(int id)
        {
            var currentUrl = url.Replace("{CurrentId}", id.ToString());
            var response = await client.GetAsync(currentUrl);
            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
