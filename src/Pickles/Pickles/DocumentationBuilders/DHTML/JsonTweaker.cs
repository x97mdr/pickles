using System;
using System.IO.Abstractions;

namespace PicklesDoc.Pickles.DocumentationBuilders.DHTML
{
    public class JsonTweaker
    {
        private readonly IFileSystem fileSystem;

        public JsonTweaker(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public void AddJsonPWrapperTo(string filePath)
        {
            var existingContent = this.fileSystem.File.ReadAllText(filePath);

            using (var s = new System.IO.StreamWriter(filePath))
            {
                s.WriteLine("jsonPWrapper (");
                s.WriteLine(existingContent);
                s.Write(");");

                s.Flush();
                s.Close();
            }
        }

        public void RenameFileTo(string oldFilePath, string newFilePath)
        {
            this.fileSystem.File.Delete(newFilePath);
            this.fileSystem.File.Move(oldFilePath, newFilePath);
        }
    }
}
