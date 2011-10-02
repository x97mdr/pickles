using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pickles
{
    public class FeatureCrawler
    {
        public void Crawl(string basePath, ICrawlerListener crawlerListener)
        {
            Crawl(new DirectoryInfo(basePath), crawlerListener);
        }

        public void Crawl(DirectoryInfo basePath, ICrawlerListener crawlerListener)
        {
            foreach (var file in basePath.GetFiles())
            {
                crawlerListener.FeatureFileFound(file);
            }

            foreach (var directory in basePath.GetDirectories())
            {
                Crawl(directory, crawlerListener);
            }
        }
    }
}
