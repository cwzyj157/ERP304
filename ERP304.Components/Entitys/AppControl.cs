﻿
using System.Collections.Generic;
using System.Xml.Serialization;
using ERP304.Components.Attributes;
using ERP304.Components.Enums;

namespace ERP304.Components.Entitys {
    #region 控件类型

    public static class ControlType {
        public const string AppPage = "page";

        public const string AppForm = "appForm";
        public const string AppFormSection = "section";
        public const string AppFormTab = "tab";
        public const string AppFormText = "text";
        public const string AppFormMemo = "memo";
        public const string AppFormPwd = "password";

        public const string AppFind = "appFind";
        public const string AppQuery = "query";
        public const string AppQueryStandard = "standard";
        public const string AppQueryAdvanced = "advanced";

        public const string AppViewList = "appViewList";
        public const string AppView = "view";

        public const string AppGrid = "appGrid";
        public const string AppGridRow = "row";
        public const string AppGridSummary = "summary";
        public const string AppGridCell = "cell";

        public const string AppGridE = "appGridE";

        public const string AppGridTree = "gridTree";

        public const string AppNavBar = "nav";
        public const string AppNavItem = "navitem";
    }

    public enum OperatorType {
        [Hint(Describe = "等于")]
        [XmlEnum(Name = "eq")]
        Eq,

        [Hint(Describe = "不等于")]
        [XmlEnum(Name = "ne")]
        NotEqual,

        [Hint(Describe = "大于")]
        [XmlEnum(Name = "gt")]
        Greater,

        [Hint(Describe = "大于等于")]
        [XmlEnum(Name = "ge")]
        GreaterAndEqual,

        [Hint(Describe = "小于")]
        [XmlEnum(Name = "lt")]
        Lower,

        [Hint(Describe = "小于等于")]
        [XmlEnum(Name = "le")]
        LowerAndEqual,

        [Hint(Describe = "类似。自动在值前后加“%”")]
        [XmlEnum(Name = "like")]
        Like,

        [Hint(Describe = "类似。自动在值前面加“%”")]
        [XmlEnum(Name = "llike")]
        LikeBefore,

        [Hint(Describe = "类似。自动在值后面加“%”")]
        [XmlEnum(Name = "rlike")]
        LikeAfter,

        [Hint(Describe = "类似。不自动加“%”")]
        [XmlEnum(Name = "nlike")]
        LikeEqual,

        [Hint(Describe = "不类似")]
        [XmlEnum(Name = "not-like")]
        NotLike,

        [Hint(Describe = "为空")]
        [XmlEnum(Name = "null")]
        Null,

        [Hint(Describe = "非空")]
        [XmlEnum(Name = "not-null")]
        NotNull,

        [Hint(Describe = "包含")]
        [XmlEnum(Name = "in")]
        In,

        [Hint(Describe = "不包含")]
        [XmlEnum(Name = "not-in")]
        NotIn,

        [Hint(Describe = "特殊操作符，直接使用给定的值作为过滤条件。")]
        [XmlEnum(Name = "replace")]
        Replace,
    }

    public enum AppFormItemType {
        [XmlEnum(Name = "text")]
        Text,

        [XmlEnum(Name = "memo")]
        Memo,

        [XmlEnum(Name = "password")]
        Password,

        [XmlEnum(Name = "number")]
        Number,

        [XmlEnum(Name = "datetime")]
        Datetime,

        [XmlEnum(Name = "radio")]
        Radio,

        [XmlEnum(Name = "select")]
        Select,

        [XmlEnum(Name = "lookup")]
        Lookup,

        [XmlEnum(Name = "hidden")]
        Hidden,

        [XmlEnum(Name = "hyperlink")]
        HyperLink,

        [XmlEnum(Name = "blank")]
        Blank
    }

    public enum AppGridCellType {
        [XmlIgnore]
        None,

        [XmlEnum(Name = "text")]
        Text,

        [XmlEnum(Name = "check")]
        Check,

        [XmlEnum(Name = "datetime")]
        Datetime,

        [XmlEnum(Name = "hyperlink")]
        HyperLink,

        [XmlEnum(Name = "layer")]
        Layer,

        [XmlEnum(Name = "number")]
        Number,

        [XmlEnum(Name = "select")]
        Select,

        [XmlEnum(Name = "select2")]
        Select2,

        [XmlEnum(Name = "texticon")]
        TextIcon,

        [XmlEnum(Name = "blank")]
        Blank
    }

    public enum RequireLevel {
        [XmlEnum(Name = "optional")]
        Optional = 0,

        [XmlEnum(Name = "required")]
        Required = 1,

        [XmlEnum(Name = "proposed")]
        Proposed = 2
    }

    public enum SerialType {
        /// <summary>
        /// 不启用
        /// </summary>
        Disabled = 0,

        /// <summary>
        /// 序号列
        /// </summary>
        SerialNo = 1,

        /// <summary>
        /// 复选框列
        /// </summary>
        CheckBox = 2
    }

    #endregion 控件类型

    public class State {
        /// <summary>
        /// SQL校验是否通过
        /// </summary>
        public bool IsSqlPassed { get; set; }

        /// <summary>
        /// 校验是否停止
        /// </summary>
        public bool IsCeased { get; set; }
    }

    /// <summary>
    /// 控件类
    /// </summary>
    [XmlType(TypeName = "control")]
    public class AppControl {
        public AppControl() {
            Id = "";
            Describe = "";
            State = new State();
        }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "datasource")]
        public DataSource DataSource { get; set; }

        /// <summary>
        /// 控件
        /// </summary>
        [XmlElement(ElementName = "list", Type = typeof(AppDropDownList))]
        [XmlElement(ElementName = "tree", Type = typeof(AppDropDownTree))]
        [XmlElement(ElementName = "gridtree", Type = typeof(AppGridTree))]
        [XmlElement(ElementName = "gridtreeedit", Type = typeof(AppGridTreeEdit))]
        [XmlElement(ElementName = "form", Type = typeof(AppForm))]
        [XmlElement(ElementName = "grid", Type = typeof(AppGrid))]
        [XmlElement(ElementName = "nav", Type = typeof(AppNavBar))]
        [XmlElement(ElementName = "query", Type = typeof(AppFind))]
        public BaseControl Control { get; set; }

        #region AppGridMenu

        [XmlElement(ElementName = "title")]
        public TextNode MenuTitle { get; set; }

        [XmlElement(ElementName = "html")]
        public TextNode Html { get; set; }

        [XmlArray(ElementName = "menu")]
        [XmlArrayItem(ElementName = "menuitem")]
        public List<MenuItem> Menutems { get; set; }

        #endregion AppGridMenu

        #region AppFormMenu

        [XmlArray(ElementName = "menus")]
        [XmlArrayItem(ElementName = "menu")]
        public List<Menu> Menus { get; set; }

        #endregion

        #region AppFormMenu与AppGridMenu公用

        [XmlArray(ElementName = "shortcuts"), XmlArrayItem(ElementName = "shortcut")]
        public List<ShortCut> ShortCuts { get; set; }

        #endregion

        #region appViewList 及 AppFind公用 视图
        [XmlElement(ElementName = "view")]
        public AppView View { get; set; }
        #endregion



        #region  XmlIgnore
        [Hint(PassValid = true)]
        [XmlIgnore]
        public State State { get; set; }

        [XmlIgnore]
        public string Verson { get; set; }

        [XmlIgnore]
        public string Describe { get; set; }

        /// <summary>
        /// 传参用
        /// </summary>
        [XmlIgnore]
        public object Token { get; set; }
        #endregion
    }

    public class ShortCut {
        public ShortCut() {
            DownIcon = "/_nav/mnuDown.gif";
        }

        [XmlAttribute(AttributeName = "id")]
        [Hint(Describe = "菜单项id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "title")]
        [Hint(Describe = "菜单名称, '-'表示分隔线", IsRequired = true)]
        public string Title { get; set; }

        [XmlAttribute(AttributeName = "tip")]
        [Hint(Describe = "鼠标移上后的提示信息")]
        public string Tip { get; set; }

        [XmlAttribute(AttributeName = "icon")]
        [Hint(Describe = "菜单项的图标")]
        public string Icon { get; set; }

        [XmlAttribute(AttributeName = "downicon")]
        [Hint(Describe = "如果该按钮有下拉菜单，定义下拉箭头图片地址。如果该节点不存在，默认/_nav/mnuDown.gif")]
        public string DownIcon { get; set; }

        [XmlAttribute(AttributeName = "active")]
        [Hint(Describe = "禁用/启用", Type = FieldType.Boolean)]
        public string Active { get; set; }

        [XmlAttribute(AttributeName = "display")]
        [Hint(Describe = "菜单的隐藏/显示", Type = FieldType.Boolean)]
        public string Display { get; set; }

        [XmlAttribute(AttributeName = "style")]
        [Hint(Describe = "自定义样式控制按钮（span）的显示风格")]
        public string Style { get; set; }

        [XmlAttribute(AttributeName = "actionid")]
        [Hint(Describe = "动作点")]
        public string ActionId { get; set; }

        [XmlAttribute(AttributeName = "action")]
        [Hint(Describe = "单击调用的函数")]
        public string Action { get; set; }
    }

    public abstract class BaseControl {
        [XmlIgnore]
        public MapControlType ControlType { get; protected set; }
    }

    /// <summary>
    /// 数据源定义节点
    /// </summary>
    public class DataSource {
        public DataSource() {
            Entity = "";
            KeyName = "";
            SqlNode = new TextNode() { Text = "" };
            Type = "";
            PageMode = "1";
            DependencySql = new List<string>();
        }

        [Hint(Describe = "3.0开始支持“SP”或“StoredProcedure”")]
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [Hint(Describe = "数据表")]
        [XmlAttribute(AttributeName = "entity")]
        public string Entity { get; set; }

        [XmlAttribute(AttributeName = "keyname")]
        public string KeyName { get; set; }

        [XmlIgnore]
        public string Sql {
            get { return SqlNode.Text; }
        }

        [XmlElement(ElementName = "sql")]
        public TextNode SqlNode { get; set; }

        /// <summary>
        /// 分页模式，默认为1
        /// </summary>
        [Hint(Describe = "分页模式，默认为0，值为1时必须无重复列", Type = FieldType.Number)]
        [XmlAttribute(AttributeName = "pagemode")]
        public string PageMode { get; set; }

        [XmlArray(ElementName = "dependencysql")]
        [XmlArrayItem(ElementName = "sql", Type = typeof(string))]
        public List<string> DependencySql { get; set; }

        [XmlElement(ElementName = "order")]
        public Order Order { get; set; }
    }

    public class Order {
        public Order() {
            Field = "";
        }

        [Hint(Describe = "排序字段")]
        [XmlAttribute(AttributeName = "field")]
        public string Field { get; set; }

        [Hint(Describe = "是否按降序排序", Type = FieldType.Boolean)]
        [XmlAttribute(AttributeName = "descending")]
        public string Descending { get; set; }
    }

    public class TextNode {
        public TextNode() {
            Text = "";
        }

        [XmlText]
        public string Text { get; set; }
    }

    public class BoolNode {
        public BoolNode() {
            Text = "false";
        }

        [Hint(Type = FieldType.Boolean)]
        [XmlText]
        public string Text { get; set; }
    }

    public class NumberNode {
        public NumberNode() {
            Text = "0";
        }

        [Hint(Type = FieldType.Number)]
        [XmlText]
        public string Text { get; set; }
    }
}