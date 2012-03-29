using System;
using System.IO;

namespace Pickles.Extensions
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

       return new Uri(fullName);
     }

     public static Uri ToUri(this FileSystemInfo instance)
     {
       DirectoryInfo di = instance as DirectoryInfo;

       if (di != null)
       {
         return ToUri(di);
       }
       
       return ToUri((FileInfo)instance);
     }

    public static Uri ToUri(this FileInfo instance)
     {
       return new Uri(instance.FullName);
     }
  }
}