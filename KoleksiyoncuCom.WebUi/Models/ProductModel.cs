using KoleksiyoncuCom.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KoleksiyoncuCom.WebUi.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Ürün adı alanı boş bırakılamaz.")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Fiyat boş bırakılamaz."), Range(1,9999999999,ErrorMessage = "Fiyat 1 ile 9999999999 değerleri arasında olmak zorunda.")]
        public decimal ProductUnitPrice { get; set; }
        [Required(ErrorMessage = "Stok bilgisi boş bırakılamaz."), Range(1,9999999999,ErrorMessage = "Stok 1 ile 9999999999 arasında olmalıdır.")]
        public int ProductUnitsInStock { get; set; }
        [Required(ErrorMessage = "Kategori boş bırakılamaz.")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Ürün görseli olmak zorunda.")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Ürün açıklaması olmak zorunda.")]
        public string Description { get; set; }
        public int AllTimeClick { get; set; }
        public int SellerId { get; set; }
        public List<Category> Category { get; set; }
    }
}
