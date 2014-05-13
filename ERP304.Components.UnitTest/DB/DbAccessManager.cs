using System.Linq;
using ERP304.Components.UnitTest.Entitys;
using Mysoft.Map.Extensions.DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace ERP304.Components.UnitTest.DB {
    public class DbAccessManager {
        private static bool s_status = false;



        public static void Init() {
            if (!s_status) {
                InitSqlConnection();
                s_status = true;
            }
        }

        private static void InitSqlConnection() {
            // 初始化数据库连接数据库
            string connectionString = (from s in System.IO.File.ReadAllLines(@"c:\test_erp_connectionString.txt")
                                       where s.StartsWith(";") == false
                                       select s
                                    ).First();

            Mysoft.Map.Extensions.Initializer.UnSafeInit(connectionString);
        }

        public static MyFunction GetPageLocation(string functionId) {
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
                        WHERE   FunctionCode = @functionId
                                AND FunctionType = '功能 '";


            MyFunction myFunction = CPQuery.From(sql, new { functionId = functionId }).ToSingle<MyFunction>();
            return myFunction;
        }
    }
    [Serializable]
    public class MyFunction {
        public string ApplicationName { get; set; }
        public string ParentFunctionName { get; set; }
        public string FunctionName { get; set; }
        public string FunctionGUID { get; set; }
        public string FunctionCode { get; set; }
        public string FunctionType { get; set; }
        public string FunctionUrl { get; set; }

        public override string ToString()
        {
            return string.Format("\t{0}\t{1}\t{2}\t{3}",
                                 new string[] {ApplicationName, ParentFunctionName, FunctionName, FunctionUrl});
        }
    }
}