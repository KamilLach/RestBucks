using System.Linq;
using Domain;

namespace Infrastructure.Persistance.Extensions
{
   public static class ProductExtension
   {
      public static Product GetByName(this IRepository<Product> a_products, string a_name)
      {
         return a_products.Retrieve(a_p => a_p.Name.ToLower() == a_name.ToLower())
                                            .FirstOrDefault();
      }
   }
}