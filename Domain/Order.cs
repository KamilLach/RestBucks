using System;
using System.Collections.Generic;
using System.Linq;
using Domain.BaseClass;
using Infrastructure.Exceptions;

namespace Domain
{
   public class Order : EntityBase, IValidable
   {
      private readonly ICollection<OrderItem> _items;

      public Order()
      {
         _items = new HashSet<OrderItem>();
         Status = OrderStatus.Unpaid;
      }

      public virtual DateTime Date { get; set; }
      public virtual OrderStatus Status { get; set; }
      public virtual Location Location { get; set; }
      public virtual string CancelReason { get; protected set; }

      public virtual ICollection<OrderItem> Items
      {
         get { return _items; }
      }

      public virtual decimal Total
      {
         get { return Items.Sum(a_i => a_i.Quantity*a_i.UnitPrice); }
      }

      public virtual Payment Payment { get; protected set; }

      #region IValidable Members

      public virtual IEnumerable<string> GetErrorMessages()
      {
         if (!Items.Any()) yield return "The order must include at least one item.";

         IEnumerable<string> itemsErrors = Items.SelectMany((a_i, a_index) => a_i.GetErrorMessages()
                                                                                 .Select(
                                                                                    a_m =>
                                                                                    string.Format("Item {0}: {1}",
                                                                                                  a_index, a_m)));

         foreach (string itemsError in itemsErrors)
         {
            yield return itemsError;
         }
      }

      #endregion

      public virtual void AddItem(OrderItem a_orderItem)
      {
         if (Status != OrderStatus.Unpaid)
         {
            throw new InvalidOrderOperationException(
               string.Format("Can't add another item to the order because it is {0}.",
                             Status.ToString().ToLower()));
         }
         a_orderItem.Order = this;
         _items.Add(a_orderItem);
      }

      public virtual void Cancel(string a_cancelReason)
      {
         if (Status != OrderStatus.Unpaid)
         {
            throw new InvalidOrderOperationException(string.Format("The order can not be canceled because it is {0}.",
                                                                   Status.ToString().ToLower()));
         }
         CancelReason = a_cancelReason;
         Status = OrderStatus.Canceled;
      }

      public virtual void Pay(string a_cardNumber, string a_cardOwner)
      {
         if (Status != OrderStatus.Unpaid)
         {
            throw new InvalidOrderOperationException(string.Format("The order can not be paid because it is {0}.",
                                                                   Status.ToString().ToLower()));
         }
         Status = OrderStatus.Paid;
         Payment = new Payment {CardOwner = a_cardOwner, CreditCardNumber = a_cardNumber};
      }

      public virtual void Finish()
      {
         if (Status != OrderStatus.Paid)
         {
            throw new InvalidOrderOperationException(string.Format(
               "The order should not be finished because it is {0}.",
               Status.ToString().ToLower()));
         }
         Status = OrderStatus.Ready;
      }
   }
}