using StoreParser.Parser.ProDjShopUrlCollector;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StoreParser.Parser
{
    public class ProductLoader
    {
        //readonly HttpClient client;

        //public ProductLoader()
        //{
        //    client = new HttpClient();
        //}

        public async Task<string> Load(string url)
        {

            string source;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
            {
                using (Stream resStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(resStream, Encoding.GetEncoding(1251));
                    source = reader.ReadToEnd();
                }
            }



            //var response = await client.GetAsync(url);

            //string source = null;

            //if (response != null && response.StatusCode == HttpStatusCode.OK)
            //{
            //    source = await response.Content.ReadAsStringAsync();
            //}

            //byte[] encodeString = Encoding.GetEncoding(1251).GetBytes(source);
            //source = Encoding.Default.GetString(encodeString);


            return source;
        }
    }
}
