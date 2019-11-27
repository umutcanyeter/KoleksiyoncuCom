using KoleksiyoncuCom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Entites
{
    public class UsersAndSellers : IEntity
    {
        public int ID { get; set; }
        public int SellerId { get; set; }
        public string UserId { get; set; }
    }
}
