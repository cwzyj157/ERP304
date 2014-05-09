using System;
using ERP304.Components.Enums;

namespace ERP304.Components.Entitys {
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "page")]
    public class MapPage {
        private string _PageXml = "";

        public MapPage() {
            this.Funcid = "";
            this.Application = "";
            this.PageTitle = "";
            this.PageUrl = "";
            this.PageXml = "";
            this.Controls = new List<AppControl>();
        }

        /// <summary>
        /// 控件整理，移除不支持的控件，给AppGrid附上标题
        /// 得到MapPage对象后都需要调用
        /// </summary>
        public void Arrange() {
            int appGridIndex = -1;
            int appGridMenuIndex = -1;
            for (int i = this.Controls.Count - 1 ; i >= 0 ; i--) {
                AppControl ac = this.Controls[i];
                if (ac.MenuTitle != null) {
                    appGridMenuIndex = i;
                }
                else if (ac.Control is AppGrid) {
                    appGridIndex = i;
                }

                if (this.IsNotSupport(ac)) {
                    this.Controls.Remove(ac);
                }

                // 将不规范的appgridmenu及appformmenu整理
                if (ac.MenuTitle != null) {
                    ArrangeAppControl(ac, MapControlType.AppGridMenu);
                }
                else if (ac.Menus != null && ac.Menus.Count > 0) {
                    ArrangeAppControl(ac, MapControlType.AppFormMenu);
                }
                else if (ac.View != null) {
                    if (ac.Control != null && ac.Control.ControlType == MapControlType.AppFind) {
                        ArrangeAppControl(ac, MapControlType.AppFind);
                    }
                    else {
                        ArrangeAppControl(ac, MapControlType.AppViewList);
                    }
                }
                else {
                    MapControlType jdControlType = getJDControlType(ac);
                    if (jdControlType != MapControlType.Loader) {
                        ArrangeAppControl(ac, jdControlType);
                    }
                }
            }

            //给appGrid加上标题
            if ((appGridIndex > -1) && (appGridMenuIndex > -1)) {
                this.Controls[appGridIndex].Describe = this.Controls[appGridMenuIndex].MenuTitle.Text;
            }
        }
        private void ArrangeAppControl(AppControl ac, MapControlType mapControlType) {
            if (mapControlType == MapControlType.AppGridMenu) {
                // 将现appControl整理为新的appControl
                AppGridMenu gridMenu = new AppGridMenu();
                gridMenu.Title = ac.MenuTitle == null ? string.Empty : ac.MenuTitle.Text;
                gridMenu.ShortCuts = ac.ShortCuts;
                gridMenu.Menutems = ac.Menutems;
                gridMenu.Html = ac.Html == null ? string.Empty : ac.Html.Text;
                ac.Control = gridMenu;
            }
            else if (mapControlType == MapControlType.AppFormMenu) {
                AppFormMenu formMenu = new AppFormMenu();
                formMenu.Menus = ac.Menus;
                formMenu.ShortCuts = ac.ShortCuts;
                ac.Control = formMenu;
            }
            else if (mapControlType == MapControlType.AppFind) {
                AppFind appFind = (ac.Control as AppFind);
                if (appFind != null) {
                    appFind.View = ac.View;
                    ac.Control = appFind;
                }
            }
            else if (mapControlType == MapControlType.AppViewList) {
                AppViewList appViewList = new AppViewList();
                appViewList.View = ac.View;
                ac.Control = appViewList;
            }
        }
        private MapControlType getJDControlType(AppControl ac) {
            // 判断是否是极端情况，gridmenu与formmenu同时配置不标准
            if (ac.MenuTitle == null
                && (ac.Menus == null || ac.Menus.Count == 0)
                && ac.ShortCuts != null && ac.ShortCuts.Count > 0) {
                if (ac.Id.IndexOf("gridmenu", StringComparison.OrdinalIgnoreCase) >= 0) {
                    return MapControlType.AppGridMenu;
                }
                else if (ac.Id.IndexOf("formmenu", StringComparison.OrdinalIgnoreCase) >= 0) {
                    return MapControlType.AppFormMenu;
                }
            }
            return MapControlType.Loader;
        }

        private bool IsNotSupport(AppControl ac) {
            return ((ac.Control == null) && (ac.ShortCuts == null));
        }

        /// 功能权限。
        [XmlAttribute(AttributeName = "actionid")]
        public string ActionId { get; set; }

        /// 页面所属的系统代码。
        [XmlAttribute(AttributeName = "application")]
        public string Application { get; set; }

        [XmlArrayItem(ElementName = "control", Type = typeof(AppControl)), XmlArray(ElementName = "controls")]
        public List<AppControl> Controls { get; set; }

        /// 页面所属的功能模块代码。
        [XmlAttribute(AttributeName = "funcid")]
        public string Funcid { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string PageId { get; set; }

        [XmlIgnore]
        public string PageTitle { get; set; }

        [XmlIgnore]
        public string PageUrl { get; set; }

        [XmlIgnore]
        public string PageXml {
            get {
                if (!(!string.IsNullOrEmpty(this._PageXml) || string.IsNullOrEmpty(this.PageUrl))) {
                    this._PageXml = this.PageUrl.Replace("aspx", "xml");
                }
                return this._PageXml;
            }
            set {
                this._PageXml = value;
            }
        }

        [XmlIgnore]
        public List<MapPage> SubPages { get; set; }
    }

}