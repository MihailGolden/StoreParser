using System;
using System.Collections.Generic;
using System.Text;
using StoreParser.Parser;
using Xunit;
using StoreParser.Parser.ProDJShopParser;


namespace StoreParser.Tests.Parser
{
    public class UrlCollectorWorkerTest
    {
        [Fact]
        public async void UrlCollectLoaderTestHtmsPageAsString()
        {
            // Arrange
            ProDjShopUrlCollectorSettings collectorSettings = new ProDjShopUrlCollectorSettings();
            ProDjShopUrlCollector collector = new ProDjShopUrlCollector();
            UrlCollectorWorker<string[]> worker = new UrlCollectorWorker<string[]>(collector, collectorSettings);
            worker.Settings = collectorSettings;
            object obj = new object();
            string[] urls = new string[]{"https://www.prodj.com.ua/studio-monitors/monkey-banana-gibbon-5-banana.html"};


            // Act
            worker.Start();

            //todo: asserts
            // Assert
            //Assert.NotNull(result);
            //Assert.IsType<string>(result);
            //Assert.NotEmpty(result);
        }
    }
}
