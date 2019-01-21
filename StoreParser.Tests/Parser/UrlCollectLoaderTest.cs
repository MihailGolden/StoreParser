using StoreParser.Parser.ProDJShopParser;
using StoreParser.Parser;
using Xunit;

namespace StoreParser.Tests.Parser
{
    public class UrlCollectLoaderTest
    {
        [Fact]
        public async void UrlCollectLoaderTestHtmsPageAsString()
        {
            // Arrange
            ProDjShopUrlCollectorSettings settings = new ProDjShopUrlCollectorSettings();
            UrlCollectLoader loader = new UrlCollectLoader(settings);
            int pageId = 5;


            // Act
            var result = await loader.GetSourceByPageIdAsync(pageId);


            // Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.NotEmpty(result);
        }
    }
}
