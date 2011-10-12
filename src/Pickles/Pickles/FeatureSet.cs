using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NGenerics.DataStructures.Trees;
using System.IO;

namespace Pickles
{
    public class FeatureSet
    {
        private readonly GeneralTree<FileInfo> m_files;
        private readonly Dictionary<string, FileInfo> m_fileLookup;

        public FeatureSet()
        {
            m_files = new GeneralTree<FileInfo>(new FileInfo("."));
            m_fileLookup = new Dictionary<string, FileInfo>();
        }

        public ITree<FileInfo> GetFeatureTree()
        {
            return m_files;
        }
    }
}
