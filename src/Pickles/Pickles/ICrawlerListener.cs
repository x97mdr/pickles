using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pickles
{
    public interface ICrawlerListener
    {
        void FeatureFileFound(FileSystemInfo file);
        IEnumerable<FileSystemInfo> GetResult();
    }
}
