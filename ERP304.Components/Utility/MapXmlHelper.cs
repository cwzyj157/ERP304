using ERP304.Components.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP304.Components.Utility {
    public class MapXmlHelper {
        public static MapPage DeserializeFromFile(string xmlPath, Encoding encoding) {
            MapPage mp = Mysoft.Map.Extensions.Xml.XmlHelper.XmlDeserializeFromFile<MapPage>(xmlPath, encoding);
            mp.Arrange();
            mp.PageXml = xmlPath;
            return mp;
        }
        public static MapPage DeserializeFromFile(string xmlPath) {
            return DeserializeFromFile(xmlPath, Encoding.Default);
        }
    }
}
