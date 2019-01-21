using AngleSharp.Parser.Html;
using StoreParser.Parser.Interfaces;
using System;

namespace StoreParser.Parser
{
    public class UrlCollectorWorker<T> where T : class
    {
        IUrlCollector<T> collector;
        IUrlCollectorSettings collectorSettings;

        UrlCollectLoader loader;

        bool isActive;

        #region Properties

        public IUrlCollector<T> Collector
        {
            get
            {
                return collector;
            }
            set
            {
                collector = value;
            }
        }

        public IUrlCollectorSettings Settings
        {
            get
            {
                return collectorSettings;
            }
            set
            {
                collectorSettings = value;
                loader = new UrlCollectLoader(value);
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }

        #endregion

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;

        public UrlCollectorWorker(IUrlCollector<T> collector)
        {
            this.collector = collector;
        }

        public UrlCollectorWorker(IUrlCollector<T> collector, IUrlCollectorSettings collectorSettings) : this(collector)
        {
            this.collectorSettings = collectorSettings;
        }

        public void Start()
        {
            isActive = true;
            Worker();
        }

        public void Abort()
        {
            isActive = false;
        }

        private async void Worker()
        {
            for (int i = collectorSettings.StartPoint; i <= collectorSettings.EndPoint; i++)
            {
                if (!isActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }

                var source = await loader.GetSourceByPageIdAsync(i);
                var domParser = new HtmlParser();

                var document = await domParser.ParseAsync(source);

                var result = collector.Collect(document);

                OnNewData?.Invoke(this, result);
            }

            OnCompleted?.Invoke(this);
            isActive = false;
        }
    }
}
