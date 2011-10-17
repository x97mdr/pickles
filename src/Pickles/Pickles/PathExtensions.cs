using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pickles
{
    public static class PathExtensions
    {
        public static string MakeRelativePath(string from, string to)
        {
            if (string.IsNullOrEmpty(from)) throw new ArgumentNullException("from");
            if (string.IsNullOrEmpty(to)) throw new ArgumentNullException("to");

            // Uri class treats paths that end in \ as directories, and without \ as files. 
            // So if its a file then we need to append the \ to make the Uri class recognize it as a directory
            string fromString = Directory.Exists(from) ? from + @"\" : from;
            string toString = Directory.Exists(to) ? to + @"\" : to;

            Uri fromUri = new Uri(fromString);
            Uri toUri = new Uri(toString);

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            return relativePath.Replace('/', Path.DirectorySeparatorChar);
        }

        public static string MakeRelativePath(FileSystemInfo from, FileSystemInfo to)
        {
            if (from == null) throw new ArgumentNullException("from");
            if (to == null) throw new ArgumentNullException("to");

            return MakeRelativePath(from.FullName, to.FullName);
        }
    }
}
