using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP304.Console.Extensions;
using NUnit.Framework;

namespace ERP304.Console.Utility {

    [TestFixture]
    public class DirectoryHelperTester {
        [Test]
        public void Run() {
            DirectoryHelper.CopyFileByExtension(@"E:\MySoftCode\项目任务\ERP3.0.4平台更新03.12\Trunk - 副本\ERP\明源整体解决方案",
                @"E:\2014年重点工作\ERP304工作项\功能导航技术验证\第一批技术验证\控件分析\ERP304所有页面使用的XML\未做结构调整前XML", ".xml");
        }
    }

    public static class DirectoryHelper {
        public static void CopyFileByExtension(string srcPath, string dstPath, params string[] extension) {
            IEnumerable<FileInfo> enumerable = new DirectoryInfo(srcPath).GetFilesByExtensions(extension);

            srcPath = Path.GetFullPath(srcPath);

            foreach (var fileInfo in enumerable)
            {
                var fullName = fileInfo.FullName;
                var subDirPath = fullName.Replace(srcPath, string.Empty)
                                         .Replace(fileInfo.Name, string.Empty).TrimStart(Path.DirectorySeparatorChar);
                
                var dstDirPath = Path.Combine(dstPath, subDirPath);

                DirectoryInfo di = CreateDir(dstDirPath);
                if (di!=null)
                {
                    var destFileName = Path.Combine(dstDirPath, fileInfo.Name);
                    fileInfo.CopyTo(destFileName);
                }
            }

        }

        public static DirectoryInfo CreateDir(string dirPath) {
            return Directory.CreateDirectory(dirPath);
        }
    }
}
