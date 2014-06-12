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

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Abstractions;
using System.Reflection;

using Stream = System.IO.Stream;
using StreamReader = System.IO.StreamReader;

namespace PicklesDoc.Pickles.DocumentationBuilders.HTML
{
    public class HtmlResourceWriter
    {
        private readonly IFileSystem fileSystem;

        public HtmlResourceWriter(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        private static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            while (true)
            {
                int read = input.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                    return;
                output.Write(buffer, 0, read);
            }
        }

        private void WriteStyleSheet(string folder, string filename)
        {
            string path = this.fileSystem.Path.Combine(folder, filename);
            using (
                var reader =
                    new StreamReader(
                        Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Resources.Html." + filename)))
            {
                this.fileSystem.File.WriteAllText(path, reader.ReadToEnd());
            }
        }

        private void WriteImage(string folder, string filename)
        {
            string path = this.fileSystem.Path.Combine(folder, filename);
            using (
                Image image =
                    Image.FromStream(
                        Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Resources.Html.images." + filename))
                )
            {
                using (var stream = this.fileSystem.File.Create(path))
                {
                    image.Save(stream, ImageFormat.Png);
                }
            }
        }

      private void WriteScript(string folder, string filename)
      {
        string path = this.fileSystem.Path.Combine(folder, filename);
        using (
            var reader =
                new StreamReader(
                    Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Resources.Html.scripts." +
                                                                              filename)))
        {
            this.fileSystem.File.WriteAllText(path, reader.ReadToEnd());
        }
      }

      private void WriteFont(string folder, string filename)
      {
          Assembly assembly = Assembly.GetExecutingAssembly();
          using (var input = assembly.GetManifestResourceStream("PicklesDoc.Pickles.Resources.Html.fonts." + filename))
          {
              using (var output = this.fileSystem.File.Create(this.fileSystem.Path.Combine(folder, filename)))
              {
                  CopyStream(input, output);
              }
          }
      }

        public void WriteTo(string folder)
        {
            this.WriteStyleSheet(folder, "master.css");
            this.WriteStyleSheet(folder, "reset.css");
            this.WriteStyleSheet(folder, "global.css");
            this.WriteStyleSheet(folder, "structure.css");
            this.WriteStyleSheet(folder, "print.css");
            this.WriteStyleSheet(folder, "font-awesome.css");

            string imagesFolder = this.fileSystem.Path.Combine(folder, "images");
            if (!this.fileSystem.Directory.Exists(imagesFolder)) this.fileSystem.Directory.CreateDirectory(imagesFolder);
            this.WriteImage(imagesFolder, "success.png");
            this.WriteImage(imagesFolder, "failure.png");
            this.WriteImage(imagesFolder, "inconclusive.png");

            string scriptsFolder = this.fileSystem.Path.Combine(folder, "scripts");
            if (!this.fileSystem.Directory.Exists(scriptsFolder)) this.fileSystem.Directory.CreateDirectory(scriptsFolder);
            this.WriteScript(scriptsFolder, "jquery.js");
            this.WriteScript(scriptsFolder, "scripts.js");

            string fontsFolder = this.fileSystem.Path.Combine(folder, "fonts");
            if (!this.fileSystem.Directory.Exists(fontsFolder)) this.fileSystem.Directory.CreateDirectory(fontsFolder);
            this.WriteFont(fontsFolder, "FontAwesome.ttf");
            this.WriteFont(fontsFolder, "fontawesome-webfont.eot");
            this.WriteFont(fontsFolder, "fontawesome-webfont.svg");
            this.WriteFont(fontsFolder, "fontawesome-webfont.ttf");
            this.WriteFont(fontsFolder, "fontawesome-webfont.woff");
        }
    }
}