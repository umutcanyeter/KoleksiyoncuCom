using System.Collections.Generic;
using System.Linq;
using KoleksiyoncuCom.Entities;

namespace KoleksiyoncuCom.WebUi.Models
{
    public class HomePageProductListViewModel
    {
        public List<Product> Product { get; set; }
        public List<Category> Category { get; set; }
    }
}