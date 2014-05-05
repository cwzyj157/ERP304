using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace ERP304.Components.Entitys
{
    [XmlType(TypeName = "attribute")]
    public class AppControlAttribute
    {
        public AppControlAttribute()
        {
            Name = "";
            Field = "";
            DataType = "";
            Attributes = new Collection<XmlAttribute>();
        }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "field")]
        public string Field { get; set; }

        [XmlAttribute(AttributeName = "datatype")]
        public string DataType { get; set; }

        [XmlAnyAttribute]
        public Collection<XmlAttribute> Attributes { get; set; }
    }
}