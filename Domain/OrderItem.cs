using System;
using System.Collections.Generic;
using System.Linq;
using Domain.BaseClass;

namespace Domain
{
    public class OrderItem : EntityBase, IValidable
    {
        public OrderItem()
        {
            Preferences = new Dictionary<string, string>();
            Quantity = 1;
        }

        public OrderItem(Product a_product, int a_quantity, decimal a_unitPrice, IDictionary<string, string> a_preferences)
        {
            Product = a_product;
            Quantity = a_quantity;
            UnitPrice = a_unitPrice;
            Preferences = a_preferences;
        }

        public virtual Product Product { get; set; }
        public virtual int Quantity { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual IDictionary<string, string> Preferences { get; set; }
        public virtual Order Order { get; set; }

        public virtual IEnumerable<string> GetErrorMessages()
        {
            if (Quantity == 0) yield return "Quantity should be greater than 0.";
            if(Product != null)
            {
                foreach (var preference in Preferences)
                {
                    if(!Product.Customizations.Any(a_c => a_c.Name.ToLower() == preference.Key.ToLower() && a_c.PossibleValues.Contains(preference.Value, StringComparer.CurrentCultureIgnoreCase)))
                    {
                        yield return string.Format("The product {0} does not have a customization: {1}/{2}.", 
                                                    Product.Name, 
                                                    preference.Key,
                                                    preference.Value);
                    }
                }
            }
        }
    }
}