using System.Xml.Serialization;

namespace Application.Dto
{
    [XmlRoot("item")]
    public class ItemRepresentation
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}