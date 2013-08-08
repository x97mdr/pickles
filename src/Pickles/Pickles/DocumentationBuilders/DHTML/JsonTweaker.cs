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

            this.fileSystem.File.WriteAllText(filePath, string.Format("jsonPWrapper ({0});", existingContent));
        }

        public void RenameFileTo(string oldFilePath, string newFilePath)
        {
            this.fileSystem.File.Delete(newFilePath);
            this.fileSystem.File.Move(oldFilePath, newFilePath);
        }
    }
}
