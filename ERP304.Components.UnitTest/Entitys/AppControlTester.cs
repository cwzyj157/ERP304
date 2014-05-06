using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ERP304.Components.Entitys;

namespace ERP304.Components.UnitTest.Entitys {
    [TestFixture]
    public class AppControlTester {
        [Test]
        public void Run()
        {
            string path = @"E:\MySoftCode\项目任务\ERP3.0.4平台更新03.12\Trunk - 副本\ERP\明源整体解决方案\Map\Cwjk\Pzgl_Htjz.xml";
            MapPage mp = Mysoft.Map.Extensions.Xml.XmlHelper.XmlDeserializeFromFile<MapPage>(path, Encoding.Default);
            
            foreach (var appControl in mp.Controls)
            {
                Console.WriteLine(appControl.Query.Standard.Items[0].Title);
            }

        }
    }
}
