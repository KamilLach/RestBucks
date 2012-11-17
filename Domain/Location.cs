using System.Xml.Serialization;

namespace Domain
{
    public enum Location
    {
        [XmlEnum("takeAway")]
        TakeAway,
        [XmlEnum("inShop")]
        InShop
    }
}