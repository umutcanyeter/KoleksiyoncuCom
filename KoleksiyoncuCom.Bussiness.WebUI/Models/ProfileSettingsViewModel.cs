using KoleksiyoncuCom.Entities;
using System.ComponentModel.DataAnnotations;

namespace KoleksiyoncuCom.Bussiness.WebUi.Models
{
    public class ProfileSettingsViewModel
    {
        public int SellerId { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string SellerName { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string Adress { get; set; }
    }
}