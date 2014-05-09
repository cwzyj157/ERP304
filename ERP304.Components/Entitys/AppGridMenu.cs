using System.Collections.Generic;
using System.Xml.Serialization;
using ERP304.Components.Enums;

namespace ERP304.Components.Entitys {
    public class AppGridMenu : BaseControl {
        public AppGridMenu() {
            Title = "";
            Html = "";
            this.ControlType = MapControlType.AppGridMenu;
        }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlArray(ElementName = "menu")]
        [XmlArrayItem(ElementName = "menuitem")]
        public List<MenuItem> Menutems { get; set; }


        [XmlElement(ElementName = "html")]
        public string Html { get; set; }


        [XmlArray(ElementName = "shortcuts")]
        [XmlArrayItem(ElementName = "shortcut")]
        public List<ShortCut> ShortCuts { get; set; }
    }
}
