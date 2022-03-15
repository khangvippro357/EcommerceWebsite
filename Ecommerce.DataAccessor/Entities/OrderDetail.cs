﻿using System;


namespace Ecommerce.DataAccessor.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid? OrderId { get; set; }
        public Order Order { get; set; }
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
        
        public Rating Rating { get; set; }

    }
}
