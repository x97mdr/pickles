using System;
using System.IO.Abstractions;

namespace PicklesDoc.Pickles.Extensions
{
    public static class UriExtensions
    {
        public static Uri ToUri(this DirectoryInfoBase instance)
        {
            string fullName = instance.FullName;

            if (!instance.FullName.EndsWith(@"\"))
            {
                fullName = fullName + @"\";
            }

            return fullName.ToFolderUri();
        }

        public static Uri ToFileUriCombined(this DirectoryInfoBase instance, string file, IFileSystem fileSystem)
        {
            string path = fileSystem.Path.Combine(instance.FullName, file);

            return path.ToFileUri();
        }

        public static Uri ToUri(this FileSystemInfoBase instance)
        {
            var di = instance as DirectoryInfoBase;

            if (di != null)
            {
                return ToUri(di);
            }

            return ToUri((FileInfoBase)instance);
        }

        public static Uri ToUri(this FileInfoBase instance)
        {
            return ToFileUri(instance.FullName);
        }

        public static Uri ToFileUri(this string instance)
        {
            return new Uri(instance);
        }

        public static Uri ToFolderUri(this string instance)
        {
            if (!instance.EndsWith(@"\"))
            {
                return new Uri(instance + @"\");
            }

            return new Uri(instance);
        }

        public static string GetUriForTargetRelativeToMe(this Uri me, FileSystemInfoBase target, string newExtension)
        {
            return target.FullName != me.LocalPath
                       ? me.MakeRelativeUri(target.ToUri()).ToString().Replace(target.Extension, newExtension)
                       : "#";
        }
    }
}