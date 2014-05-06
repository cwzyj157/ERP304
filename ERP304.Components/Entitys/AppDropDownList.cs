
using System.Collections.Generic;
using System.Xml.Serialization;
using ERP304.Components.Attributes;


namespace ERP304.Components.Entitys
{
    public sealed class AppDropDownList : BaseControl
    {
        public AppDropDownList()
        {
            TextField = new TextNode() { Text = "text" };
            ValueField = new TextNode() { Text = "value" };
        }

        [Hint(Describe = "标题")]
        [XmlElement(ElementName = "title")]
        public TextNode Title { get; set; }

        [Hint(Describe = "拼写查询过滤条件使用的字段")]
        [XmlElement(ElementName = "field")]
        public TextNode Field { get; set; }

        [Hint(Describe = "下拉选项text绑定字段，默认为text")]
        [XmlElement(ElementName = "textfield")]
        public TextNode TextField { get; set; }

        [Hint(Describe = "下拉选项value绑定字段，默认为value")]
        [XmlElement(ElementName = "valuefield")]
        public TextNode ValueField { get; set; }

        [Hint(Describe = "下拉框项")]
        [XmlArray(ElementName = "options")]
        [XmlArrayItem(ElementName = "option")]
        public List<PropertyOption> Options { get; set; }
    }

    public sealed class PropertyOption
    {
        [XmlText]
        public string Text { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }
}