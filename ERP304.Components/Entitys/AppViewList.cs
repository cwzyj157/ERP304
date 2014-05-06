using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using ERP304.Components.Attributes;
using ERP304.Components.Utility;
using Mysoft.Map.Extensions.Xml;

namespace ERP304.Components.Entitys {

    public sealed class AppViewList {
        public AppViewList() {
            AppFindViewItems = new List<AppViewListItem>();
            ResultXmlUrl = "";
            GroupId = "";
        }

        [Hint(Describe = "查询结果视图")]
        [XmlAttribute(AttributeName = "resultxmlurl")]
        public string ResultXmlUrl { get; set; }

        [Hint(Describe = "历史查询所属的页面")]
        [XmlAttribute(AttributeName = "groupid")]
        public string GroupId { get; set; }

        [XmlElement(ElementName = "item")]
        public List<AppViewListItem> AppFindViewItems { get; set; }
    }

    public sealed class AppViewListItem {
        /// <summary>
        /// 视图 id，定位视图用
        /// </summary>
        [XmlAttribute(AttributeName = "xmlid")]
        [Hint(Describe = "视图id")]
        public string XmlId { get; set; }

        /// <summary>
        /// 视图对应的 XML 文件地址
        /// </summary>
        [XmlAttribute(AttributeName = "xmlurl")]
        [Hint(Describe = "视图XML路径")]
        public string XmlUrl { get; set; }

        [XmlText]
        public string Title { get; set; }

        [XmlAttribute(AttributeName = "viewid")]
        [Hint(Describe = "视图id")]
        public string ViewId { get; set; }

        [XmlAttribute(AttributeName = "selected")]
        [Hint(Describe = "是否默认视图", Type = FieldType.Boolean)]
        public string IsSelected { get; set; }

        private MapPage _SubPage = null;

        [XmlIgnore]
        public MapPage SubPage {
            get {
                if (!string.IsNullOrEmpty(XmlUrl) && _SubPage == null) {
                    try {
                        string path = PathHelper.MapPath(XmlUrl);

                        _SubPage = XmlHelper.XmlDeserializeFromFile<MapPage>(path, System.Text.Encoding.Default);
                        if (_SubPage != null) {
                            _SubPage.Arrange();
                        }
                    }
                    catch (Exception) {
                    }
                }

                return _SubPage;
            }
            set { _SubPage = value; }
        }
    }
}
