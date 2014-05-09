using System.Collections.Generic;
using System.Xml.Serialization;
using ERP304.Components.Attributes;
using ERP304.Components.Enums;

namespace ERP304.Components.Entitys {
    public class AppFormMenu : BaseControl {

        public AppFormMenu() {
            this.ControlType = MapControlType.AppFormMenu;
        }

        [XmlArray(ElementName = "menus")]
        [XmlArrayItem(ElementName = "menu")]
        public List<Menu> Menus { get; set; }

        [XmlArray(ElementName = "shortcuts")]
        [XmlArrayItem(ElementName = "shortcut")]
        public List<ShortCut> ShortCuts { get; set; }
    }

}