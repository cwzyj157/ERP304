using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP304.Console.Extensions {
    public static class DirectoryInfoExtensions {
        public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dir, params string[] extensions) {
            if (extensions == null)
                throw new ArgumentNullException("extensions");
            IEnumerable<FileInfo> files = dir.EnumerateFiles("*.*", SearchOption.AllDirectories);
            return files.Where(f => extensions.Contains(f.Extension));
        }

        public static IEnumerable<FileInfo> GetFilesByExtensions_Lower4(this DirectoryInfo dir, params string[] extensions) {
            if (extensions == null)
                throw new ArgumentNullException("extensions");
            IEnumerable<FileInfo> files = Enumerable.Empty<FileInfo>();
            foreach (string ext in extensions) {
                files = files.Concat(dir.GetFiles(ext, SearchOption.AllDirectories));
            }
            return files;
        }
    }
}
