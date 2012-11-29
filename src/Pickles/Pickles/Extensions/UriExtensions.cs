using System;
using System.IO;

namespace PicklesDoc.Pickles.Extensions
{
    public static class UriExtensions
    {
        public static Uri ToUri(this DirectoryInfo instance)
        {
            string fullName = instance.FullName;

            if (!instance.FullName.EndsWith(@"\"))
            {
                fullName = fullName + @"\";
            }

            return fullName.ToFolderUri();
        }

        public static Uri ToFileUriCombined(this DirectoryInfo instance, string file)
        {
            string path = Path.Combine(instance.FullName, file);

            return path.ToFileUri();
        }

        public static Uri ToUri(this FileSystemInfo instance)
        {
            var di = instance as DirectoryInfo;

            if (di != null)
            {
                return ToUri(di);
            }

            return ToUri((FileInfo) instance);
        }

        public static Uri ToUri(this FileInfo instance)
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

        public static string GetUriForTargetRelativeToMe(this Uri me, FileSystemInfo target, string newExtension)
        {
            return target.FullName != me.LocalPath
                       ? me.MakeRelativeUri(target.ToUri()).ToString().Replace(target.Extension, newExtension)
                       : "#";
        }
    }
}