using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using ERP304.Components.Attributes;

namespace ERP304.Components.Entitys {
    class GridTree {
    }

    public class AppGridTreeExpandLevel {
        [XmlAttribute(AttributeName = "syncload")]
        public string IsSyncload { get; set; }
    }

    public class AppGridTreeColor {
        [XmlElement(ElementName = "divborder")]
        public ColorItem DivBorder { get; set; }

        [XmlElement(ElementName = "tableborder")]
        public ColorItem TableBorder { get; set; }

        [Hint(Describe = "节点背景色")]
        [XmlElement(ElementName = "level")]
        public List<ColorItem> Levels { get; set; }
    }
    public class ColorItem {
        [XmlText]
        public string ColorValue { get; set; }
    }

    public class AppGridTreeRow {
        public AppGridTreeRow() {
            Attributes = new List<AppControlAttribute>();
            Cells = new List<AppGridTreeCell>();
        }

        [XmlArray(ElementName = "attributes")]
        [XmlArrayItem(ElementName = "attribute", Type = typeof(AppControlAttribute))]
        public List<AppControlAttribute> Attributes { get; set; }

        [XmlElement(ElementName = "cell")]
        public List<AppGridTreeCell> Cells { get; set; }
    }

    public class AppGridTreeCell : AppGridCellBase {
        public AppGridTreeCell() {
            //CellType = "text";
            Field = "";
            Title = "";
        }

        [Hint(Describe = "是否冻结列", Type = FieldType.Boolean)]
        [XmlAttribute(AttributeName = "fixed")]
        public string IsFixed { get; set; }
    }
}
