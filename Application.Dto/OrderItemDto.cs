using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Application.Dto
{
   public class OrderItemDto
   {
      #region Properties

      public string Name { get; set; }
      public int Quantity { get; set; }
      public List<KeyValuePair<string, string>> Preferences { get; set; }
      #endregion

      #region Constructors

      public OrderItemDto()
      {
         Preferences = new List<KeyValuePair<string, string>>();
      }

      #endregion
   }
}