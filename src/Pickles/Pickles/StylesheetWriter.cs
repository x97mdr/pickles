using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pickles
{
    public class StylesheetWriter
    {
        public void WriteTo(string path)
        {
            using (var reader = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Resources.styles.css")))
            using (var writer = new StreamWriter(path))
            {
                writer.Write(reader.ReadToEnd());
                writer.Flush();
                writer.Close();
                reader.Close();
            }
        }
    }
}
