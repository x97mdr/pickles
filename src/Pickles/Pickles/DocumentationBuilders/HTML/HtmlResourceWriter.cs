#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

namespace Pickles
{
    public class HtmlResourceWriter
    {
        private void WriteStyleSheet(string folder, string filename)
        {
            string path = Path.Combine(folder, filename);
            using (
                var reader =
                    new StreamReader(
                        Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Resources." + filename)))
            using (var writer = new StreamWriter(path))
            {
                writer.Write(reader.ReadToEnd());
                writer.Flush();
                writer.Close();
                reader.Close();
            }
        }

        private void WriteImage(string folder, string filename)
        {
            string path = Path.Combine(folder, filename);
            using (
                Image image =
                    Image.FromStream(
                        Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Resources.images." + filename))
                )
            {
                image.Save(path, ImageFormat.Png);
            }
        }

      private void WriteScript(string folder, string filename)
      {
        string path = Path.Combine(folder, filename);
        using (
          var reader =
            new StreamReader(
              Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Resources.scripts." +
                                                                        filename)))
        using (var writer = new StreamWriter(path))
        {
          writer.Write(reader.ReadToEnd());
          writer.Flush();
          writer.Close();
          reader.Close();
        }
      }

      private void WriteFont(string folder, string filename)
        {
            string path = Path.Combine(folder, filename);
            using (
                var reader =
                    new StreamReader(
                        Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Resources.fonts." +
                                                                                  filename)))
            using (var writer = new StreamWriter(path))
            {
                writer.Write(reader.ReadToEnd());
                writer.Flush();
                writer.Close();
                reader.Close();
            }
        }

        public void WriteTo(string folder)
        {
            WriteStyleSheet(folder, "master.css");
            WriteStyleSheet(folder, "reset.css");
            WriteStyleSheet(folder, "global.css");
            WriteStyleSheet(folder, "structure.css");
            WriteStyleSheet(folder, "print.css");
            WriteStyleSheet(folder, "font-awesome.css");

            string imagesFolder = Path.Combine(folder, "images");
            if (!Directory.Exists(imagesFolder)) Directory.CreateDirectory(imagesFolder);
            WriteImage(imagesFolder, "success.png");
            WriteImage(imagesFolder, "failure.png");
            WriteImage(imagesFolder, "inconclusive.png");

            string scriptsFolder = Path.Combine(folder, "scripts");
            if (!Directory.Exists(scriptsFolder)) Directory.CreateDirectory(scriptsFolder);
            WriteScript(scriptsFolder, "jquery.js");
            WriteScript(scriptsFolder, "scripts.js");

            string fontsFolder = Path.Combine(folder, "fonts");
            if (!Directory.Exists(fontsFolder)) Directory.CreateDirectory(fontsFolder);
            WriteFont(fontsFolder, "FontAwesome.ttf");
            WriteFont(fontsFolder, "fontawesome-webfont.eot");
            WriteFont(fontsFolder, "fontawesome-webfont.svg");
            WriteFont(fontsFolder, "fontawesome-webfont.ttf");
            WriteFont(fontsFolder, "fontawesome-webfont.woff");
        }
    }
}