using System.Collections.Generic;

namespace Infrastructure
{
   public interface IResourceLinkProvider
   {
      IEnumerable<ILink> GetLinks<TEntity>(TEntity a_entity);
   }
}