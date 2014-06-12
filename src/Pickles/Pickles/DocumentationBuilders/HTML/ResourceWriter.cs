using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Abstractions;
using System.Reflection;

namespace PicklesDoc.Pickles.DocumentationBuilders.HTML
{
  public class ResourceWriter
  {
    protected IFileSystem fileSystem;

    private readonly string namespaceOfResources;

    public ResourceWriter(IFileSystem fileSystem, string namespaceOfResources)
    {
      this.fileSystem = fileSystem;
      this.namespaceOfResources = namespaceOfResources;
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

    protected void WriteStyleSheet(string folder, string filename)
    {
      string path = this.fileSystem.Path.Combine(folder, filename);
      using (
        var reader =
          new StreamReader(
            Assembly.GetExecutingAssembly().GetManifestResourceStream(this.namespaceOfResources + filename)))
      {
        this.fileSystem.File.WriteAllText(path, reader.ReadToEnd());
      }
    }

    protected void WriteImage(string folder, string filename)
    {
      string path = this.fileSystem.Path.Combine(folder, filename);
      using (
        Image image =
          Image.FromStream(
            Assembly.GetExecutingAssembly().GetManifestResourceStream(this.namespaceOfResources + "images." + filename))
        )
      {
        using (var stream = this.fileSystem.File.Create(path))
        {
          image.Save(stream, ImageFormat.Png);
        }
      }
    }

    protected void WriteScript(string folder, string filename)
    {
      string path = this.fileSystem.Path.Combine(folder, filename);
      using (
        var reader =
          new StreamReader(
            Assembly.GetExecutingAssembly().GetManifestResourceStream(this.namespaceOfResources + "scripts." + filename)))
      {
        this.fileSystem.File.WriteAllText(path, reader.ReadToEnd());
      }
    }

    protected void WriteFont(string folder, string filename)
    {
      Assembly assembly = Assembly.GetExecutingAssembly();
      using (var input = assembly.GetManifestResourceStream(this.namespaceOfResources + "fonts." + filename))
      {
        using (var output = this.fileSystem.File.Create(this.fileSystem.Path.Combine(folder, filename)))
        {
          CopyStream(input, output);
        }
      }
    }
  }
}