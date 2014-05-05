using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NUnit.Framework;
using System.IO;
using System.Xml;

namespace ERP304.Console.Controls {
    [TestFixture]
    public class AppFindTester {
        [Test]
        public void Run() {

            #region
            //            var xml = @"<advanced>
            //  <items>
            //    <item field='title' type='text' operator='like' title='标题' />
            //    <item field='Content' type='text' operator='like' title='新闻内容'></item>
            //    <item field='createUserName' type='text' operator='like' title='创建人' />
            //    <item field='OwnerDept' type='text' operator='like' title='所属部门' />
            //    <item field='km_NewsSort.Name' type='text' operator='like' title='新闻分类' />
            //    <item field='KeyWord' type='text' operator='like' title='关键字'></item>
            //    <item field='ReleaseTime' type='datetime' time='1' title='发布时间' operator='ge' />
            //    <item field='ReleaseTime' type='datetime' time='1' title='到' operator='le' />
            //  </items>
            //</advanced>";

            //            var xe = XDocument.Parse(xml);
            //            var attrs = xe.Descendants("item").Attributes("field");

            //            attrs = attrs.Distinct(new FindItemAttrComparer());

            //            foreach (var xAttribute in attrs) {
            //                System.Console.WriteLine(xAttribute.Value);
            //            }

            //var result =
            //    new AppFind().LoadXml(
            //        @"E:\MySoftCode\项目任务\ERP3.0.4平台更新03.12\Trunk - 副本\ERP\明源整体解决方案\Map\Xmjd\XMPlan\WorkResult.xml");
            //foreach (var findItemInfo in result) {
            //    System.Console.WriteLine(findItemInfo.ToString());
            //}
            #endregion

            new AppFind().FindMaxItem();
        }
    }

    public class AppFind {
        /// <summary>
        /// 获取查询条件最大数
        /// </summary>
        public void FindMaxItem() {
            string dictPath = @"E:\MySoftCode\项目任务\ERP3.0.4平台更新03.12\Trunk - 副本\ERP\明源整体解决方案\Map";

            var files = Directory.EnumerateFiles(dictPath, "*.xml", SearchOption.AllDirectories);

            var itemInfos = Enumerable.Empty<FindItemInfo>();

            foreach (var file in files) {
                itemInfos =
                    itemInfos.Concat(LoadXml(file));
            }
            //List<FindItemInfo> list = itemInfos.ToList();

            //foreach (var findItemInfo in list) {
            //    System.Console.WriteLine(findItemInfo.AdvancedElement.ToString());
            //    System.Console.WriteLine("\r\n");
            //}
           
            var maxStanderdItem = itemInfos.Max(m => m.StandardFieldCount);
            System.Console.WriteLine("标准查询最大层级：" + maxStanderdItem);
            // 获取层级为最大时XML内容
            foreach (var findItemInfo in itemInfos.Where(m => m.StandardFieldCount == maxStanderdItem)) {
                System.Console.WriteLine(findItemInfo.FileName);
                System.Console.WriteLine(findItemInfo.StandardElement.ToString());
                System.Console.WriteLine("\r\n");
            }


            var maxAdvancedItem = itemInfos.Max(m => m.AdvancedFieldCount);
            System.Console.WriteLine("高级查询最大层级：" + maxAdvancedItem);
            // 获取层级为最大时XML内容
            foreach (var findItemInfo in itemInfos.Where(m => m.AdvancedFieldCount == maxAdvancedItem)) {
                System.Console.WriteLine(findItemInfo.FileName);
                System.Console.WriteLine(findItemInfo.AdvancedElement.ToString());
                System.Console.WriteLine("\r\n");
            }
        }
        
        private IEnumerable<FindItemInfo> LoadXml(string file) {

            XDocument doc = null;
            try {
                doc = XDocument.Load(file);
            }
            catch (Exception ex) {
                yield break;
            }
            var result = from p in doc.Descendants("page").Descendants("control")
                         where p.HasAttributes == true && p.Attribute("id").Value.ToLower() == "appfind"
                         select p;

            foreach (var element in result) {
                var standard = element.Descendants("standard").FirstOrDefault();
                var advanced = element.Descendants("advanced").FirstOrDefault();
                if (standard == null || advanced == null) yield break;

                var standardItem = standard.Descendants("item");
                var advancedItem = advanced.Descendants("item");

                yield return new FindItemInfo() {
                    FileName = file,
                    StandardElement = standard,
                    StandardFindItem = standardItem,
                    StandardFieldCount = standardItem.Attributes("field").Distinct(new FindItemAttrComparer()).Count(),

                    AdvancedElement = advanced,
                    AdvancedFindItem = advancedItem,
                    AdvancedFieldCount = advancedItem.Attributes("field").Distinct(new FindItemAttrComparer()).Count(),
                };
            }
        }

    }
    public class FindItemInfo {
        public string FileName;
        public XElement StandardElement;
        public int StandardFieldCount;
        public IEnumerable<XElement> StandardFindItem;

        public XElement AdvancedElement;
        public int AdvancedFieldCount;
        public IEnumerable<XElement> AdvancedFindItem;
    }
    public class FindItemAttrComparer : IEqualityComparer<XAttribute> {
        public bool Equals(XAttribute x, XAttribute y) {
            if (x == null && y == null) {
                return false;
            }
            return x.Value == y.Value;
        }

        public int GetHashCode(XAttribute obj) {
            return obj.Value.GetHashCode();
        }
    }
}
