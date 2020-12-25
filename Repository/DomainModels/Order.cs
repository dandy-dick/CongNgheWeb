using System;
using System.Collections.Generic;

namespace Repository.DomainModels
{
    public class Order
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public string DeliveryAddress { get; set; }

        public string ReceiverFullName { get; set; }

        public string ReceiverPhoneNumber { get; set; }

        public string Description { get; set; }

        public string State { get; set; }

        //public DateTime CreatedDate { get; set; }
        //public DateTime UpdatedDate { get; set; }
    }

    public class OrderItem
    {
        public string Id { get; set; }
        public string OrderId { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
