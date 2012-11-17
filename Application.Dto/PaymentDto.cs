using System.Xml.Serialization;

namespace Application.Dto
{
   [XmlRoot("payment", Namespace = "http://restbuckson.net")]
   public class PaymentDto
   {
      [XmlElement("card-number")]
      public string CreditCardNumber { get; set; }

      [XmlElement("card-owner")]
      public string CardOwner { get; set; }
   }
}