using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP304.Components.Enums;
using ERP304.Components.UnitTest.DB;
using ERP304.Components.Utility;
using ERP304.Console.Extensions;
using NUnit.Framework;
using ERP304.Components.Entitys;
using System.IO;
using Mysoft.Map.Extensions.DAL;

namespace ERP304.Components.UnitTest.Entitys {
    [TestFixture]
    public class AppControlTester {
        [Test]
        public void Run() {
            string path = @"E:\MySoftCode\项目任务\ERP3.0.4平台更新03.12\Trunk - 副本\ERP\明源整体解决方案\Map\Cbgl\HTDL\Contract_ChangeJbr.xml";

            MapPage mp = MapXmlHelper.DeserializeFromFile(path, Encoding.UTF8);

            foreach (var appControl in mp.Controls) {
                if (appControl.Control != null) {
                    System.Console.WriteLine(appControl.Control.ControlType.ToString());
                    System.Console.WriteLine(appControl.Control.ToString());
                }
            }
        }

        [Test]
        public void Run1() {

            Logger msgLogger = new Logger("平台控件使用情况.log");
            Logger exceptionLogger = new Logger("异常.log");

            StringBuilder msgBuilder = new StringBuilder();

            #region 平台有多少控件

            msgLogger.Write("\r\n== 平台有多少控件 ==================================================\r\n");

            string format = "平台共有{0}个控件，分别如下：";
            MapControlType[] enumValues = EnumHelper<MapControlType>.GetValues();
            msgBuilder.AppendFormat(format, enumValues.Length).AppendLine();
            var index = 1;
            foreach (var enumValue in enumValues) {
                msgBuilder.AppendFormat("\t{0}({1})", enumValue.ToString(),
                                        enumValue.GetAttachedData(MapControlTypeAttachData.Name));
                if (index % 4 == 0) {
                    msgBuilder.AppendLine();
                }
                index++;
            }
            msgBuilder.AppendLine();
            msgLogger.Write(msgBuilder.ToString());
            #endregion



            IEnumerable<FileInfo> enumerable = new DirectoryInfo(
                @"E:\MySoftCode\项目任务\ERP3.0.4平台更新03.12\Trunk - 副本\ERP\明源整体解决方案\Map")
                .GetFilesByExtensions(".xml");

            List<MapPage> lstmp = new List<MapPage>();

            foreach (var fileInfo in enumerable) {
                try {
                    lstmp.Add(MapXmlHelper.DeserializeFromFile(fileInfo.FullName, Encoding.UTF8));
                }
                catch (Exception ex) {
                    exceptionLogger.Write(string.Format("{0},异常：\r\n{1}", fileInfo.FullName, ex.Message));
                }
            }

            #region 每个页面使用了哪些控件

            msgLogger.Write("\r\n== 每个页面使用了哪些控件 ==================================================\r\n");
            msgBuilder.Length = 0;
            format = "页面{0}使用了以下控件：";
            foreach (var mapPage in lstmp) {
                msgBuilder.AppendFormat(format, mapPage.PageXml).AppendLine();
                foreach (var appControl in mapPage.Controls) {
                    if (appControl.Control != null) {
                        msgBuilder.AppendFormat("\t{0}({1})", appControl.Control.ControlType.ToString(),
                                                appControl.Control.ControlType.GetAttachedData(
                                                    MapControlTypeAttachData.Name));
                    }
                }
                msgBuilder.AppendLine().AppendLine();
            }
            msgBuilder.AppendLine();
            msgLogger.Write(msgBuilder.ToString());

            #endregion


            DbAccessManager.Init();



            #region 依控件类型查找控件在哪些页面中使用

            msgLogger.Write("\r\n== 每个控件在哪些页面中使用 ==================================================\r\n");
            msgBuilder.Length = 0;
            format = "控件{0}({1})，在以下{2}个页面中使用";
            foreach (var enumValue in enumValues) {
                var mapPage = lstmp.Where(m => m.Controls.Any(n => n.Control != null && n.Control.ControlType == enumValue));
                var name = enumValue.GetAttachedData(MapControlTypeAttachData.Name);

                msgBuilder.AppendLine(string.Format(format, enumValue.ToString(), name, mapPage.Count()));

                foreach (var page in mapPage) {
                    if (string.IsNullOrEmpty(page.Funcid) == true) {
                        exceptionLogger.Write(string.Format("文件{0}未配置functionid", page.PageXml));
                        continue;
                    }

                    var funcIdAry = page.Funcid.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var s in funcIdAry) {
                        MyFunction myFunction = DbAccessManager.GetPageLocation(s);
                        if (myFunction != null) {
                            msgBuilder.AppendLine(myFunction.ToString());
                        }
                    }
                }

                msgBuilder.AppendLine();
            }
            msgBuilder.AppendLine();
            msgLogger.Write(msgBuilder.ToString());

            #endregion


            #region 控件组合使用情况
            msgLogger.Write("\r\n== 控件组合使用明细 ==================================================\r\n");
            msgBuilder.Length = 0;

            List<MapConntrolTypeGroup> mapConntrolTypeGroups = new List<MapConntrolTypeGroup>();

            foreach (var page in lstmp)
            {
                var result = page.Controls.Where(n => n.Control != null)
                                 .Select(n => n.Control.ControlType).Distinct().ToList();

                bool exists = mapConntrolTypeGroups.Any(n => CompareToList(n.controlTypes, result) == true);
                if (exists == false) {
                    mapConntrolTypeGroups.Add(new MapConntrolTypeGroup() { controlTypes = result });
                }
            }

            msgBuilder.AppendLine(string.Format("平台控件有如下{0}组合使用情况", mapConntrolTypeGroups.Count));
            foreach (var mapConntrolTypeGroup in mapConntrolTypeGroups) {
                msgBuilder.AppendLine(mapConntrolTypeGroup.ToString());
            }
            msgBuilder.AppendLine();
            msgLogger.Write(msgBuilder.ToString());
            #endregion

            #region 控件每种组合页面使用情况
            msgLogger.Write("\r\n== 控件每种组合页面使用情况 ==================================================\r\n");
            msgBuilder.Length = 0;

            foreach (var mapConntrolTypeGroup in mapConntrolTypeGroups) {
                msgBuilder.AppendLine(mapConntrolTypeGroup.ToString());
                int count = 0;
                foreach (var page in lstmp) {
                    var controlTypes = page.Controls.Where(n => n.Control != null)
                                           .Select(n => n.Control.ControlType).ToList();
                    if (CompareToList(mapConntrolTypeGroup.controlTypes, controlTypes) == true) {
                        if (string.IsNullOrEmpty(page.Funcid) == true) {
                            exceptionLogger.Write(string.Format("文件{0}未配置functionid", page.PageXml));
                            continue;
                        }
                        var funcIdAry = page.Funcid.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var s in funcIdAry) {
                            MyFunction myFunction = DbAccessManager.GetPageLocation(s);
                            if (myFunction != null) {
                                msgBuilder.Append(myFunction.ToString()).AppendLine();
                                count++;
                            }
                        }
                        /*msgBuilder.Append("\t").Append(page.PageXml).AppendLine();*/
                    }
                }
                System.Console.WriteLine(mapConntrolTypeGroup.ToString() + " 共 " + count);

                msgBuilder.AppendLine();
            }
            msgBuilder.AppendLine();
            msgLogger.Write(msgBuilder.ToString());
            #endregion

        }
        private bool CompareToList(List<MapControlType> A, List<MapControlType> B) {
            if (A.Count != B.Count) {
                return false;
            }

            foreach (var mapControlType in A) {
                if (B.Contains(mapControlType) == false) {
                    return false;
                }
            }
            return true;
        }
    }
}

public class MapConntrolTypeGroup {
    public List<MapControlType> controlTypes;

    public override string ToString() {
        if (controlTypes != null && controlTypes.Count > 0) {
            StringBuilder sb = new StringBuilder(controlTypes.Count * 5);
            controlTypes.ForEach(n => sb.AppendFormat("\t{0}({1})", n, n.GetAttachedData(MapControlTypeAttachData.Name)));
            return sb.ToString();
        }
        return string.Empty;
    }
}

