using KoleksiyoncuCom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Entities
{
    public class Cart : IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CardItem> CartItems { get; set; }
    }
}
