using System.Xml.Serialization;

namespace Domain
{
    public enum OrderStatus
    {
        OrderCreated,
        Unpaid,
        Paid,
        Ready,
        Canceled,
        Delivered
    }
}