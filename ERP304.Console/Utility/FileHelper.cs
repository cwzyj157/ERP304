using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERP304.Console.Extensions;
using NUnit.Framework;

namespace ERP304.Console.Utility {
    [TestFixture]
    public class FileHelperTester {
        [Test]
        public void Run() {
            var srcPath = Path.GetFullPath(@"c:\x\\k");
            System.Console.WriteLine(srcPath);

        }
    }

    internal sealed class FileHelper {
        public static void CopyFileByExtension(string dirPath, params string[] extension) {
            IEnumerable<FileInfo> enumerable = new DirectoryInfo(dirPath).GetFilesByExtensions(extension);


        }
    }
}
