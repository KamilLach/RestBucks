using System.Collections.Generic;

namespace Infrastructure
{
   public interface IResourceLinkProvider
   {
      IEnumerable<ILink> GetLinks();
   }
}