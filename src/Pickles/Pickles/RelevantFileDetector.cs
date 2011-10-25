using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pickles
{
    public class RelevantFileDetector
    {
        public bool IsRelevant(FileInfo file)
        {
            switch(file.Extension.ToLowerInvariant())
            {
                case ".feature":
                case ".md":
                    return true;
            }

            return false;
        }
    }
}
