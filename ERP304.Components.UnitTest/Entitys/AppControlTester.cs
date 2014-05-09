using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP304.Components.Enums;
using ERP304.Components.Utility;
using ERP304.Console.Extensions;
using NUnit.Framework;
using ERP304.Components.Entitys;
using System.IO;
using  Mysoft.Map.Extensions.DAL;

namespace ERP304.Components.UnitTest.Entitys {
    [TestFixture]
    public class AppControlTester {
        [Test]
        public void Run() {
            string path = @"E:\MySoftCode\项目任务\ERP3.0.4平台更新03.12\Trunk - 副本\ERP\明源整体解决方案\Map\Hyxt\XTGL\SetMemberCardMode.xml";

            MapPage mp = MapXmlHelper.DeserializeFromFile(path, Encoding.UTF8);

            foreach (var appControl in mp.Controls) {
                System.Console.WriteLine(appControl.Control.ControlType.ToString());

                System.Console.WriteLine(appControl.Control.ToString());
            }
        }

        [Test]
        public void Run1()
        {
            //InitSqlConnection();

            IEnumerable<FileInfo> enumerable = new DirectoryInfo(
                @"E:\MySoftCode\项目任务\ERP3.0.4平台更新03.12\Trunk - 副本\ERP\明源整体解决方案\Map\Hyxt")
                .GetFilesByExtensions(".xml");

            List<MapPage> lstmp = new List<MapPage>();

            foreach (var fileInfo in enumerable) {
                try {
                    lstmp.Add(MapXmlHelper.DeserializeFromFile(fileInfo.FullName, Encoding.UTF8));
                    System.Console.WriteLine(fileInfo.FullName);
                }
                catch (Exception ex) {
                    //   System.Console.WriteLine(fileInfo.FullName + " ex:" + ex.Message);
                }
            }

            foreach (var mapPage in lstmp)
            {
                foreach (var appControl in mapPage.Controls)
                {
                    System.Console.WriteLine(appControl.Control.ControlType);
                }
            }


            // 依控件类型查找控件在哪些页面中使用
            string format = "控件{0}({1})，在以下{2}个页面中使用";
            MapControlType[] enumValues = EnumHelper<MapControlType>.GetValues();
            foreach (var enumValue in enumValues)
            {
                var mapPage = lstmp.Where(m => m.Controls.Any(n => n.Control.ControlType == enumValue));
                var name = enumValue.GetAttachedData(MapControlTypeAttachData.Name);
                format = string.Format(format, enumValue.ToString(), name, mapPage.Count());

                foreach (var page in mapPage)
                {
                    System.Console.WriteLine(page.Funcid);
                }
            }





            //foreach (var fileInfo in enumerable) {
            //    MapPage mp = MapXmlHelper.DeserializeFromFile(fileInfo.FullName, Encoding.UTF8);

            //    foreach (var appControl in mp.Controls) {
            //        System.Console.WriteLine(appControl.Control.ControlType.ToString());

            //        System.Console.WriteLine(appControl.Control.ToString());
            //    }
            //}
        }
        private void InitSqlConnection()
        {
            // 初始化数据库连接数据库
            string connectionString = (from s in System.IO.File.ReadAllLines(@"c:\SmokingTest.DAL.connectionString.txt")
                                       where s.StartsWith(";") == false
                                       select s
                                    ).First();

            Mysoft.Map.Extensions.Initializer.UnSafeInit(connectionString);
        }

        private void GetPageLocation(string functionId)
        {
            string sql = @"SELECT 
                                Application ,
                                ApplicationName ,
                                ParentFunctionName ,
                                FunctionName ,
                                FunctionGUID ,
                                FunctionCode ,
                                FunctionType,
                                FunctionUrl
                        FROM    e_myFunction
                        WHERE   FunctionCode = @functionId'
                                AND FunctionType = '功能 '";


            myFunction myFunction = CPQuery.From(sql, new {functionId = functionId}).ToSingle<myFunction>();
        }
    }

    [Serializable]
    public class myFunction
    {
        public string ApplicationName { get; set; }
        public string ParentFunctionName { get; set; }
        public string FunctionName { get; set; }
        public string FunctionGUID { get; set; }
        public string FunctionCode { get; set; }
        public string FunctionType { get; set; }
        public string FunctionUrl { get; set; }
    }
}
