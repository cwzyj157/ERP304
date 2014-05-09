using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using ERP304.Components.Attributes;

namespace ERP304.Components.Entitys {
    public class Menu {
        [XmlAttribute(AttributeName = "id")]
        [Hint(Describe = "菜单ID", IsRequired = false)]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "title")]
        [Hint(Describe = "菜单名称, '-'表示分隔线", IsRequired = true)]
        public string Title { get; set; }

        [XmlAttribute(AttributeName = "display")]
        [Hint(Describe = "控制隐藏/显示", IsRequired = false)]
        public string Display { get; set; }

        [XmlAttribute(AttributeName = "downicon")]
        [Hint(Describe = "下拉箭头图片地址", IsRequired = false)]
        public string DownIcon { get; set; }

        [XmlElement(ElementName = "menuitem")]
        public List<MenuItem> MenuItems { get; set; }
    }

    public class MenuItem {
        /// <summary>
        /// 菜单项 id，可选
        /// </summary>
        [XmlAttribute(AttributeName = "id")]
        [Hint(Describe = "菜单项id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "title")]
        [Hint(Describe = "菜单名称, '-'表示分隔线", IsRequired = true)]
        public string Title { get; set; }

        [XmlAttribute(AttributeName = "icon")]
        [Hint(Describe = "菜单项的图标")]
        public string Icon { get; set; }

        [XmlAttribute(AttributeName = "display")]
        [Hint(Describe = "菜单的隐藏/显示", Type = FieldType.Boolean)]
        public string Display { get; set; }

        [XmlAttribute(AttributeName = "actionid")]
        [Hint(Describe = "动作点")]
        public string ActionId { get; set; }

        [XmlAttribute(AttributeName = "active")]
        [Hint(Describe = "禁用/启用", Type = FieldType.Boolean)]
        public string Active { get; set; }


        [XmlAttribute(AttributeName = "action")]
        [Hint(Describe = "单击调用的函数")]
        public string Action { get; set; }

        [XmlElement(ElementName = "menuitem")]
        public List<MenuItem> MenuItems { get; set; }
    }
}
