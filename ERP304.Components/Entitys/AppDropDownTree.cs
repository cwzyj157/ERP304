﻿using System.Collections.Generic;
using System.Xml.Serialization;
using ERP304.Components.Attributes;
using ERP304.Components.Enums;

namespace ERP304.Components.Entitys {
    public class AppDropDownTree : BaseControl {
        public AppDropDownTree() {
            TextField = new TextNode() { Text = "text" };
            ValueField = new TextNode() { Text = "value" };
            LevelField = new TextNode() { Text = "" };

            this.ControlType = MapControlType.AppDropDownTree;
        }

        [Hint(Describe = "显示文本字段")]
        [XmlElement(ElementName = "textfield")]
        public TextNode TextField { get; set; }

        [Hint(Describe = "值字段")]
        [XmlElement(ElementName = "valuefield")]
        public TextNode ValueField { get; set; }

        [Hint(Describe = "层级编码字段。层级编码以点号(.)分隔。 ")]
        [XmlElement(ElementName = "levelfield")]
        public TextNode LevelField { get; set; }

        [Hint(Describe = "默认值。允许为空。")]
        [XmlElement(ElementName = "defaulttext")]
        public TextNode DefaultText { get; set; }

        [Hint(Describe = "默认层级编码。允许为空。 ")]
        [XmlElement(ElementName = "defaultlevel")]
        public TextNode DefaultLevel { get; set; }

        [Hint(Describe = "选中项前面的图标")]
        [XmlElement(ElementName = "icon")]
        public TextNode Icon { get; set; }

        [Hint(Describe = "选中项的链接地址，单击选中项 open 窗口打开该页面。 ")]
        [XmlElement(ElementName = "url")]
        public Url Url { get; set; }

        [Hint(Describe = "单击下树节点（非文本前的加号），是否展开下级。 ")]
        [XmlElement(ElementName = "clickexpand")]
        public BoolNode ClickExpand { get; set; }

        [Hint(Describe = "树默认展开的级别。0 表示根节点。 ")]
        [XmlElement(ElementName = "defaultexpandlevel")]
        public TextNode DefaultExpandLevel { get; set; }

        [Hint(Describe = "下拉框显示行数，超出显示滚动条。")]
        [XmlElement(ElementName = "maxrows")]
        public NumberNode MaxRows { get; set; }

        [Hint(Describe = "根节点显示文本。")]
        [XmlElement(ElementName = "roottext")]
        public TextNode RootText { get; set; }

        [Hint(Describe = "定义当点击下拉图标时触发的事件函数。")]
        [XmlElement(ElementName = "ondropdown")]
        public TextNode OnDropDown { get; set; }

        [Hint(Describe = "定义当选中下拉框节点，修改 INPUT 文本框的值之前触发的事件函数。")]
        [XmlElement(ElementName = "onbeforechange")]
        public TextNode OnBeforeChange { get; set; }

        [Hint(Describe = "定义当点击 INPUT 文本框中的超链接文本时触发的事件函数。")]
        [XmlElement(ElementName = "onnavigate")]
        public TextNode OnNavigate { get; set; }

        [Hint(Describe = "定义当选中下拉框节点，修改 INPUT 文本框的值之后触发的事件函数。")]
        [XmlElement(ElementName = "onquery")]
        public TextNode OnQuery { get; set; }

        [Hint(Describe = "定义替换查询条件格式。关键字：[text] 用文本替换、[value] 用值替换、[code] 用层级代码替换。 ")]
        [XmlElement(ElementName = "queryreplace")]
        public TextNode QueryReplace { get; set; }
    }

    public class Url {
        [Hint(Describe = "链接文件地址")]
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }

        [XmlElement(ElementName = "param")]
        public List<Param> Params { get; set; }
    }

    public class Param {
        [Hint(Describe = "后缀参数名称")]
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [Hint(Describe = "后缀参数的值")]
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }
}
