
using System.Collections.Generic;
using System.Xml.Serialization;
using ERP304.Components.Attributes;
using ERP304.Components.Enums;

namespace ERP304.Components.Entitys {
    /// <summary>
    /// appForm控件
    /// </summary>
    public class AppForm : BaseControl {
        public AppForm() {
            Tabs = new List<AppFormTab>();
            ShowTab = "false";
            this.ControlType = MapControlType.AppForm;
        }

        [Hint(Describe = "是否输出标签头", Type = FieldType.Boolean)]
        [XmlAttribute(AttributeName = "showtab")]
        public string ShowTab { get; set; }

        [XmlElement(ElementName = "tab")]
        public List<AppFormTab> Tabs { get; set; }
    }

    public class AppFormTab {
        public AppFormTab() {
            Title = "";
            Sections = new List<AppFormSection>();
            Display = "false";
        }

        [Hint(Describe = "是否有选中标签", Type = FieldType.Boolean)]
        [XmlAttribute(AttributeName = "display")]
        public string Display { get; set; }

        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "section")]
        public List<AppFormSection> Sections { get; set; }
    }

    public class AppFormSection {
        public AppFormSection() {
            Title = "";
            SecId = "";
            Display = "true";
            Cols = "2";
            TitleWidth = "115";
            Items = new List<AppFormItem>();
        }

        [Hint(Describe = "控件标题")]
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }

        [Hint(Describe = "是否显示区域标题", Type = FieldType.Boolean)]
        [XmlAttribute(AttributeName = "showtitle")]
        public string ShowTitle { get; set; }

        [Hint(Describe = "指定每行包含的录入控件数量", Type = FieldType.Number)]
        [XmlAttribute(AttributeName = "cols")]
        public string Cols { get; set; }

        [Hint(Describe = "控件前面的标题所占的列宽", Type = FieldType.Number)]
        [XmlAttribute(AttributeName = "titlewidth")]
        public string TitleWidth { get; set; }

        [Hint(Describe = "单元格高度", Type = FieldType.Custom, Regex = @"^(\s)*|(\d+(px|pt|%)?)$")]
        [XmlAttribute(AttributeName = "height")]
        public string Height { get; set; }

        [Hint(Describe = "单元格边距")]
        [XmlAttribute(AttributeName = "cellpadding")]
        public string CellPadding { get; set; }

        [Hint(Describe = "Section ID")]
        [XmlAttribute(AttributeName = "secid")]
        public string SecId { get; set; }

        [Hint(Describe = "是否显示标题分隔条", Type = FieldType.Boolean)]
        [XmlAttribute(AttributeName = "showbar")]
        public string ShowBar { get; set; }

        /// 是否显示标题分隔条
        [Hint(Describe = "是否显示", Type = FieldType.Boolean)]
        [XmlAttribute(AttributeName = "display")]
        public string Display { get; set; }

        [XmlElement(ElementName = "item")]
        public List<AppFormItem> Items { get; set; }
    }



    /// <summary>
    /// 每个子控件在反序列化的时候无法区分开，除非重写序列化
    /// 因此都合并为这个类
    /// </summary>
    public class AppFormItem {
        public AppFormItem() {
            Title = "";
            Type = AppFormItemType.Text.ToString();
            Field = "";
            Updateapi = "1";
            Createapi = "1";
            Req = "0";
            Colspan = "1";
            Href = "?";
            Target = "_blank";
            Options = new List<SelectOption>();
            Time = "";
        }

        /// <summary>
        /// “text”表示控件为文本框控件
        /// “memo”表示控件为文本区控件。
        /// “password”表示控件为密码录入控件。
        /// “number”表示控件为数字录入控件。
        /// “datetime”表示控件为日期录入控件。
        /// </summary>
        [XmlAttribute(AttributeName = "type")]
        [Hint(Describe = "控件类型", EnumType = typeof(AppFormItemType), EnumValueType = EnumValueType.Text
            , InvalidMessage = "控件类型支持text, memo, number等，请查阅SDK")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "name")]
        [Hint(Describe = "控件名")]
        public string Name { get; set; }

        /// <summary>
        /// 依次找field,defaultvalue,editvalue,找到不为空的输出
        /// </summary>
        [XmlAttribute(AttributeName = "field")]
        [Hint(Describe = "控件值")]
        public string Field { get; set; }

        [XmlAttribute(AttributeName = "defaultvalue")]
        [Hint(Describe = "默认值")]
        public string DefaultValue { get; set; }

        [XmlAttribute(AttributeName = "editvalue")]
        [Hint(Describe = "修改记录")]
        public string EditValue { get; set; }

        [XmlAttribute(AttributeName = "title")]
        [Hint(Describe = "控件标题")]
        public string Title { get; set; }

        [XmlAttribute(AttributeName = "colspan")]
        [Hint(Describe = "控件所占列宽", Type = FieldType.Number)]
        public string Colspan { get; set; }

        /// 是否必填项。0 为非必填，1 为必填，2 为建议填写，默认值为 0 。
        [XmlAttribute(AttributeName = "req")]
        [Hint(Describe = "是否必填项", EnumType = typeof(RequireLevel), EnumValueType = EnumValueType.Value
            , InvalidMessage = "0 为非必填，1 为必填，2 为建议填写，默认值为 0 。")]
        public string Req { get; set; }

        [Hint(Describe = "单元格高度", Type = FieldType.Custom, Regex = @"^(\s)*|(\d+(px|pt|%)?)$")]
        [XmlAttribute(AttributeName = "height")]
        public string Height { get; set; }

        [Hint(Describe = "控件属性，number控件的内置属性：acc:小数保留位, grp:千分号 ")]
        [XmlElement(ElementName = "attribute")]
        public AppControlAttribute OtherAttributes { get; set; }

        #region blank

        [XmlAttribute(AttributeName = "align")]
        [Hint(Describe = "对齐方式，仅blank有效")]
        public string Align { get; set; }

        [XmlAttribute(AttributeName = "html")]
        [Hint(Describe = "html，仅blank有效")]
        public string Html { get; set; }

        #endregion blank

        #region hyperlink

        /// <summary>
        /// 新增记录时检查defaulttext，否则检查edittext，如果不是新增记录，且没有edittext则取textfield
        /// </summary>
        [XmlAttribute(AttributeName = "defaulttext")]
        [Hint(Describe = "新增记录默认值")]
        public string DefaultText { get; set; }

        [XmlAttribute(AttributeName = "edittext")]
        [Hint(Describe = "修改记录默认值")]
        public string EditText { get; set; }

        [XmlAttribute(AttributeName = "href")]
        [Hint(Describe = "href")]
        public string Href { get; set; }

        [XmlAttribute(AttributeName = "target")]
        [Hint(Describe = "target")]
        public string Target { get; set; }

        /// <summary>
        /// radio, hyperlink, lookup
        /// </summary>
        [XmlAttribute(AttributeName = "onclick")]
        [Hint(Describe = "onclick事件")]
        public string OnClick { get; set; }

        [XmlAttribute(AttributeName = "onmouseover")]
        [Hint(Describe = "onmouseover事件")]
        public string OnMouseOver { get; set; }

        [XmlAttribute(AttributeName = "onmouseout")]
        [Hint(Describe = "onmouseout事件")]
        public string OnMouseOut { get; set; }

        #endregion hyperlink

        #region 非blank, hyperlink控件

        [XmlAttribute(AttributeName = "createapi")]
        [Hint(Describe = "新增记录时，控件是否可填。0 为只读，1 为可填，默认值为 1")]
        public string Createapi { get; set; }

        [XmlAttribute(AttributeName = "updateapi")]
        [Hint(Describe = "修改记录时，控件是否可填。0 为只读，1 为可填，默认值为 1")]
        public string Updateapi { get; set; }

        #endregion 非blank, hyperlink控件

        #region text, password, number

        [XmlAttribute(AttributeName = "assistanticon")]
        [Hint(Describe = "辅助录入图标")]
        public string AssistantIcon { get; set; }

        [Hint(Describe = "图标宽度")]
        [XmlAttribute(AttributeName = "iconwidth")]
        public string IconWidth { get; set; }

        [Hint(Describe = "图标对齐方式(css align属性)")]
        [XmlAttribute(AttributeName = "iconalign")]
        public string IconAlign { get; set; }

        [XmlAttribute(AttributeName = "iconclick")]
        [Hint(Describe = "点击图标事件")]
        public string OnIconClick { get; set; }

        [XmlAttribute(AttributeName = "icontip")]
        [Hint(Describe = "图标TIP")]
        public string IconTip { get; set; }

        #endregion text, password

        #region datetime

        [XmlAttribute(AttributeName = "time")]
        [Hint(Describe = "日期格式,0:年－月－日,1:年－月－日　时：分,2:年－月－日　时：分：秒")]
        public string Time { get; set; }

        #endregion datetime

        #region select

        [XmlAttribute(AttributeName = "onchange")]
        [Hint(Describe = "onchange事件")]
        public string OnChange { get; set; }

        /// <summary>
        /// radio, select
        /// </summary>
        [XmlElement(ElementName = "option")]
        [Hint(Describe = "下拉选项")]
        public List<SelectOption> Options { get; set; }

        [XmlAttribute(AttributeName = "sql")]
        [Hint(Describe = "onchange事件")]
        public string Sql { get; set; }

        #endregion select

        #region hyperlink, select

        [XmlAttribute(AttributeName = "textfield")]
        [Hint(Describe = "非新增时文本值，或select控件备选文本取值的数据列名")]
        public string TextField { get; set; }

        #endregion hyperlink, select

        #region lookup

        [XmlAttribute(AttributeName = "detailpage")]
        [Hint(Describe = "非新增时文本值，或select控件备选文本取值的数据列名")]
        public string DetailPage { get; set; }

        [XmlAttribute(AttributeName = "lookupstyle")]
        [Hint(Describe = "multi：多选，默认单选")]
        public string LookupStyle { get; set; }

        [XmlAttribute(AttributeName = "icon")]
        [Hint(Describe = "图标")]
        public string Icon { get; set; }

        [XmlAttribute(AttributeName = "lookupbrowse")]
        [Hint(Describe = "弹出窗口是否直接显示结果，0:出现查找框，查找后出结果， 1:直接出结果")]
        public string LookupBrowse { get; set; }

        [XmlAttribute(AttributeName = "rowslimit")]
        [Hint(Describe = "返回行数限制，多选时才有效")]
        public string RowsLimit { get; set; }

        [XmlAttribute(AttributeName = "lookupclass")]
        [Hint(Describe = "指定 Lookup 界面定义的文件，在 /_controls/lookup/mapxml 目录下")]
        public string LookupClass { get; set; }

        [XmlAttribute(AttributeName = "features")]
        [Hint(Describe = "弹出界面样式（高度、宽度等）")]
        public string Features { get; set; }

        [XmlAttribute(AttributeName = "lookupcustom")]
        [Hint(Describe = "自定义 Lookup 界面")]
        public string LookupCustom { get; set; }

        [XmlAttribute(AttributeName = "additionalparams")]
        [Hint(Describe = "参数 additionalparams")]
        public string AdditionalParams { get; set; }

        [XmlAttribute(AttributeName = "onbeforeselect")]
        [Hint(Describe = "弹出 Lookup 界面前触发，如果 event.returnValue == false，则取消弹出 Lookup 界面")]
        public string OnBeforeSelect { get; set; }

        [XmlAttribute(AttributeName = "onafterselect")]
        [Hint(Describe = "弹出 Lookup 界面后触发，返回的 LookupItems 值保存在 event.result 中")]
        public string OnAfterSelect { get; set; }

        #endregion lookup
    }

    public class SelectOption {
        [XmlAttribute(AttributeName = "value")]
        [Hint(Describe = "option value")]
        public string Value { get; set; }

        [XmlText]
        [Hint(Describe = "option text")]
        public string Text { get; set; }
    }

    public class AppFromFunction {
        [XmlAttribute(AttributeName = "assembly")]
        [Hint(Describe = "option value")]
        public string Assembly { get; set; }

        [XmlAttribute(AttributeName = "invokeclass")]
        [Hint(Describe = "option value")]
        public string InvokeClass { get; set; }

        [XmlAttribute(AttributeName = "invokefunction")]
        [Hint(Describe = "option value")]
        public string InvokeFunction { get; set; }

        [XmlAttribute(AttributeName = "valuefield")]
        [Hint(Describe = "函数返回 DataTable 中作为备选项值的列")]
        public string ValueField { get; set; }

        [XmlArrayItem(ElementName = "param")]
        [Hint(Describe = "函数参数")]
        public List<FunctionParam> Params { get; set; }

    }

    public class FunctionParam {
        [XmlText]
        public string Text { get; set; }
    }
}