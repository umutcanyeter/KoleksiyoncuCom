using KoleksiyoncuCom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Entities
{
    public class Seller : IEntity
    {
        public int SellerId { get; set; }
        public string NameAndSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAdress { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public decimal Rate { get; set; }
        public bool VerifiedSeller { get; set; }
    }
}
