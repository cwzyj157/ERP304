using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ERP304.Components.Utility {
    public sealed class PathHelper {
        public static string MapPath(string path) {
            HttpContext context = HttpContext.Current;
            if (context != null) {
                path = context.Server.MapPath(path);
            }
            else {
                if (path.EndsWith("/") == true || path.EndsWith("\\") == true) {
                    path = path.Substring(1);
                }
                // 设置当前路径
                path = System.IO.Path.Combine("", path).Replace("/", "\\");
            }
            return path;
        }
    }
}
