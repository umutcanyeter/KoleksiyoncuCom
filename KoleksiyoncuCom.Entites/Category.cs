using KoleksiyoncuCom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Entities
{
    public class Category : IEntity
    {
        //Category entity for product categories.
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
