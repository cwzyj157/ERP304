using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ERP304.Components {

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

}
