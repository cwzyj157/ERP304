using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP304.Components.UnitTest {
    public class Logger {

        private readonly string filePath = string.Empty;

        public Logger(string fileName) {
            filePath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        /// <summary>
        /// 写到文件
        /// </summary>
        /// <param name="message">要写的内容</param>
        public void Write(string message) {
            using (StreamWriter writer = new StreamWriter(this.filePath, true, Encoding.UTF8)) {
                writer.WriteLine(message);
            }
        }
    }
}
