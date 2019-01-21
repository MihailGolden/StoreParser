using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StoreParser.Parser
{
    public class ProductLoader
    {
        public async Task<string> LoadAsync(string url)
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
            return source;
        }
    }
}
