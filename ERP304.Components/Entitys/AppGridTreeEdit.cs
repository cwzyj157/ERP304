using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using ERP304.Components.Enums;

namespace ERP304.Components.Entitys {
    [XmlRoot(ElementName = "gridtreeedit")]
    public class AppGridTreeEdit : BaseControl {
        public AppGridTreeEdit() {
            this.ControlType = MapControlType.AppGridTreeEdit;
        }
        /// <summary>
        /// 网格树默认展开级别
        /// </summary>
        [XmlElement(ElementName = "defaultexpandlevel")]
        public AppGridTreeExpandLevel ExpandLevel { get; set; }

        [XmlElement(ElementName = "colors")]
        public AppGridTreeColor Colors { get; set; }

        [XmlElement(ElementName = "row")]
        public AppGridTreeRow Row { get; set; }

    }
}
