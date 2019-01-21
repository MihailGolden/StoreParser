using StoreParser.Models;
using StoreParser.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StoreParser.Tests.Parser
{
    public class ImageDownloaderTest
    {
        [Fact]
        public async void ImageDownloaderLoadImage()
        {
            // Arrange
            ImageDownloader loader = new ImageDownloader();
            string url = "https://www.prodj.com.ua/img/img2b/2015/04/20150428170457.jpg";

            // Act
            Image result = await loader.DownloadImageAsync(url);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Image>(result);
            Assert.NotEmpty(result.Data);
            Assert.IsType<Guid>(result.Id);
        }
    }
}
