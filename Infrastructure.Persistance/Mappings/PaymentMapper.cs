using Domain;
using FluentNHibernate.Mapping;

namespace Infrastructure.Persistance.Mappings
{
   public class PaymentMapper : ClassMap<Payment>
   {
      public PaymentMapper()
      {
         Id(a_x => a_x.Id);
         Version(a_x => a_x.Version);
         Map(a_x => a_x.CardOwner);
         Map(a_x => a_x.CreditCardNumber);
      }
   }
}