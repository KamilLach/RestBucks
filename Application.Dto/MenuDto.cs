using System.Xml.Serialization;

namespace Application.Dto
{
    [XmlRoot("menu")]
    public class MenuRepresentation
    {
        [XmlElement("item")]
        public ItemRepresentation[] Items { get; set; }
    }
}