using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using ERP304.Components.Attributes;
using ERP304.Components.Enums;
using ERP304.Components.Utility;
using Mysoft.Map.Extensions.Xml;

namespace ERP304.Components.Entitys {

    public class AppViewList : BaseControl {
        public AppViewList() {
            this.ControlType = MapControlType.AppViewList;
        }
        [XmlIgnore]
        public AppView View { get; set; }
    }
}
