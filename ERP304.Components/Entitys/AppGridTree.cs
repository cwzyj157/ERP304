
using System.Collections.Generic;
using System.Xml.Serialization;
using ERP304.Components.Attributes;
using ERP304.Components.Enums;

namespace ERP304.Components.Entitys {
    [XmlRoot(ElementName = "gridtree")]
    public class AppGridTree : BaseControl {
        public AppGridTree() {
            this.ControlType = MapControlType.AppGridTree;
        }
        /// <summary>
        /// 网格树默认展开级别
        /// </summary>
        [XmlElement(ElementName = "defaultexpandlevel")]
        public AppGridTreeExpandLevel ExpandLevel { get; set; }

        [XmlElement(ElementName = "colors")]
        public AppGridTreeColor Colors { get; set; }

        /// <summary>
        /// 冻结列标题
        /// </summary>
        [XmlElement(ElementName = "fixedtitles")]
        public AppGridTreeFixedTitles FixedTitles { get; set; }

        /// <summary>
        /// 非冻结列标题
        /// </summary>
        [XmlElement(ElementName = "titles")]
        public AppGridTreeTitles Titles { get; set; }

        [XmlElement(ElementName = "row")]
        public AppGridTreeRow Row { get; set; }

        [XmlElement(ElementName = "queryreplace")]
        public AppGridTreeQueryReplace QueryPlace { get; set; }

        [XmlAttribute(AttributeName = "mutiSelect")]
        public string IsSupportMultiSelect { get; set; }

        [XmlAttribute(AttributeName = "syncload")]
        public string IsSyncload { get; set; }

    }

    public class AppGridTreeFixedTitles {
        [XmlElement(ElementName = "tr")]
        public List<AppGridTreeFixedTitleTr> Tr { get; set; }
    }

    public class AppGridTreeFixedTitleTr {
        [XmlElement(ElementName = "td")]
        public List<AppGridTreeFixedTitleTd> Td { get; set; }
    }

    public class AppGridTreeFixedTitleTd {
        [XmlText]
        public string Text { get; set; }
    }

    public class AppGridTreeTitles {
        [XmlElement(ElementName = "tr")]
        public List<AppGridTreeFixedTitleTr> Tr { get; set; }
    }

    public class AppGridTreeQueryReplace {
        [XmlText]
        public string Text { get; set; }
    }

}