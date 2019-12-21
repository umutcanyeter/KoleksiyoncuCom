using KoleksiyoncuCom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Entities
{
    public class CardItem : IEntity
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Cart Cart{ get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
    }
}
