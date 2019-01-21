using StoreParser.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StoreParser.Tests.Parser
{
    public class ProductLoaderTest
    {
        [Fact]
        public async void ProductLoaderLoadHtmlPageAsString()
        {
            // Arrange
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            ProductLoader loader = new ProductLoader();
            string url = "https://www.prodj.com.ua/studio-monitors/brand:monkey-banana/";
            string startsWith = "<!DOCTYPE html>";
            string endsWith = "</html>";

            // Act
            string result = await loader.LoadAsync(url);


            // Assert
            Assert.NotNull(result);

            Assert.IsType<string>(result);
            Assert.EndsWith(endsWith, result);
            Assert.StartsWith(startsWith, result);
        }
    }
}
