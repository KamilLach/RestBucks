using System.Xml.Serialization;

namespace Infrastructure
{
   public interface ILink
   {
      /// <summary>
      /// Indicates a resource with which the consumer can 
      /// interact to progress the application protocol.
      /// </summary>
      [XmlAttribute(AttributeName = "uri")]
      string Uri { get; set; }

      /// <summary>
      /// The definitions of the markup values imply which HTTP verb 
      /// to use when following the link, as  well as required HTTP 
      /// headers, and the structure of the payload.
      /// </summary>
      [XmlAttribute(AttributeName = "rel")]
      string Relation { get; set; }

      /// <summary>
      /// If a request requires an entity body, the link element will 
      /// contain a mediaType attribute that declares the format of the 
      /// request payload. If a request does not require an entity body, 
      /// the mediaType attribute will be absent.
      /// </summary>
      [XmlAttribute(AttributeName = "mediaType")]
      string MediaType { get; set; }
   }
}