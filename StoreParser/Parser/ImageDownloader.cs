using StoreParser.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreParser.Parser
{
    public class ImageDownloader
    {
        readonly HttpClient client = new HttpClient();

        public async Task<Image> DownloadImageAsync(string imageUrl)
        {
            byte[] imageData = null;
            try
            {
                using (var httpResponse = await client.GetAsync(imageUrl))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {

                        imageData =  await httpResponse.Content.ReadAsByteArrayAsync();
                        Image image = new Image() { Data = imageData, Url = imageUrl, Id = Guid.NewGuid() };
                        return image;
                    }
                    else
                    {
                        //Url is Invalid
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                //Handle Exception
                return null;
            }
        }
    }
}
