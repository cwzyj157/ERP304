using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP304.Console.Extensions;
using System.Xml;
using NUnit.Framework;

namespace ERP304.Console {
    [TestFixture]
    public class MapXmlHelperTester {
        [Test]
        public void Run() {
            MapXmlHelper.Clearup(@"E:\2014年重点工作\ERP304工作项\功能导航技术验证\第一批技术验证\控件分析\ERP304所有页面使用的XML\进行结构微调后XML");
        }
    }
    internal sealed class MapXmlHelper {
        private static readonly string AppGridMenuName = "gridmenu";
        private static readonly string AppFormMenuName = "formmenu";

        /// <summary>
        /// 整理ERP的xml结构，使结构标准，并且适用于序列化及反序列化
        /// </summary>
        public static void Clearup(string dirPath) {

            XmlDocument document = new XmlDocument();

            IEnumerable<FileInfo> enumerable = new DirectoryInfo(dirPath).GetFilesByExtensions(".xml");
            foreach (var fileInfo in enumerable) {
                try {
                    document.Load(fileInfo.FullName);

                    if (document.DocumentElement == null ||
                        string.Compare(document.DocumentElement.Name, "page", StringComparison.OrdinalIgnoreCase) != 0)
                        continue;

                    var xmlNodeList = document.DocumentElement.SelectNodes("//control");
                    if (xmlNodeList != null && xmlNodeList.Count > 0) {
                        foreach (var xmlNode in xmlNodeList) {
                            ClearupControl(xmlNode as XmlElement);
                        }
                    }
                    document.Save(fileInfo.FullName);

                }
                catch (Exception ex) {
                    System.Console.WriteLine(string.Format("FileName:{0},Exception:{1}", fileInfo.FullName, ex.Message));
                }
            }
        }
        private static void ClearupControl(XmlElement xe) {
            if (xe != null) {
                string id = xe.GetAttribute("id");
                // 处理appGridMenu
                if (id.IndexOf(AppGridMenuName, StringComparison.OrdinalIgnoreCase) >= 0) {
                    ClearUpAppGridMenu(xe);
                }
                else if (id.IndexOf(AppFormMenuName, StringComparison.OrdinalIgnoreCase) >= 0) {
                    ClearUpAppFormMenu(xe);
                }
            }
        }
        private static void ClearUpAppGridMenu(XmlElement xe) {
            SetInnerXml(xe, AppGridMenuName);
        }
        private static void ClearUpAppFormMenu(XmlElement xe) {
            SetInnerXml(xe, AppFormMenuName);
        }
        private static void SetInnerXml(XmlElement xe, string nodeName) {
            if (xe != null) {
                XmlNode xn = xe.FirstChild;
                if (xn != null) {
                    var name = xn.Name;
                    if (string.Compare(name, nodeName, StringComparison.OrdinalIgnoreCase) != 0) {
                        xe.InnerXml = string.Format("<{0}>{1}</{0}>", nodeName, xe.InnerXml);
                    }
                }
            }
        }
    }
}
