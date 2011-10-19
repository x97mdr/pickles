using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pickles
{
    public class StylesheetWriter
    {
        private void Write(string folder, string filename)
        {
            string path = Path.Combine(folder, filename);
            using (var reader = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Resources." + filename)))
            using (var writer = new StreamWriter(path))
            {
                writer.Write(reader.ReadToEnd());
                writer.Flush();
                writer.Close();
                reader.Close();
            }
        }

        public string WriteTo(string folder)
        {
            Write(folder, "master.css");
            Write(folder, "reset.css");
            Write(folder, "global.css");
            Write(folder, "structure.css");

            return Path.Combine(folder, "master.css");
        }
    }
}
