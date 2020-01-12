﻿using KoleksiyoncuCom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Entities
{
    public class OrderItem : IEntity
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int SellerId { get; set; }
    }
}
