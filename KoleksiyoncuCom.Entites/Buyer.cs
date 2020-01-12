using KoleksiyoncuCom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Entities
{
    public class Buyer :IEntity
    {
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public string ProfileImageUrl { get; set; }
        public string LastLoginDate { get; set; }
        public string RegistrationDate { get; set; }
    }
}
