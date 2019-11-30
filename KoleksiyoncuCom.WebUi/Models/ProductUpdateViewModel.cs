using System.Collections.Generic;
using KoleksiyoncuCom.Entities;

namespace KoleksiyoncuCom.WebUi.Models
{
    public class ProductUpdateViewModel
    {
        public Product Product { get; set; }
        public List<Category> Category { get; set; }
    }
}