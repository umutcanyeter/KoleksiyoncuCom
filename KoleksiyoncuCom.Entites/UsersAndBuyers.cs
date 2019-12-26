using KoleksiyoncuCom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Entities
{
    public class UsersAndBuyers : IEntity
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int BuyerId { get; set; }
    }
}
