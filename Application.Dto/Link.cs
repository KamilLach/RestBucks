using System;
using System.Xml.Serialization;
using Infrastructure;

namespace Application.Dto
{
   [Serializable]
   public class Link : ILink
   {
      public Link()
      {
      }

      public Link(string a_uri, string a_relation, string a_mediaType)
      {
         Uri = a_uri;
         Relation = a_relation;
         MediaType = a_mediaType;
      }

      #region ILink Members

      /// <summary>
      /// Indicates a resource with which the consumer can 
      /// interact to progress the application protocol.
      /// </summary>
      [XmlAttribute(AttributeName = "uri")]
      public string Uri { get; set; }

      /// <summary>
      /// The definitions of the markup values imply which HTTP verb 
      /// to use when following the link, as  well as required HTTP 
      /// headers, and the structure of the payload.
      /// </summary>
      [XmlAttribute(AttributeName = "rel")]
      public string Relation { get; set; }

      /// <summary>
      /// If a request requires an entity body, the link element will 
      /// contain a mediaType attribute that declares the format of the 
      /// request payload. If a request does not require an entity body, 
      /// the mediaType attribute will be absent.
      /// </summary>
      [XmlAttribute(AttributeName = "mediaType")]
      public string MediaType { get; set; }

      #endregion

      public override string ToString()
      {
         return string.Format("Media Type: {0}; Relation: {1}; Uri: {2}", MediaType, Relation, Uri);
      }

      #region Equality members

      public bool Equals(Link a_other)
      {
         if (ReferenceEquals(null, a_other)) return false;
         if (ReferenceEquals(this, a_other)) return true;
         return Equals(a_other.Uri, Uri) && Equals(a_other.Relation, Relation) && Equals(a_other.MediaType, MediaType);
      }

      public override bool Equals(object a_obj)
      {
         if (ReferenceEquals(null, a_obj)) return false;
         if (ReferenceEquals(this, a_obj)) return true;
         if (a_obj.GetType() != typeof (Link)) return false;
         return Equals((Link) a_obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            int result = (Uri != null ? Uri.GetHashCode() : 0);
            result = (result*397) ^ (Relation != null ? Relation.GetHashCode() : 0);
            result = (result*397) ^ (MediaType != null ? MediaType.GetHashCode() : 0);
            return result;
         }
      }

      #endregion
   }
}