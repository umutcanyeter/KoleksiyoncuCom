using System.ComponentModel.DataAnnotations;

namespace KoleksiyoncuCom.WebUi.Bussiness.Models
{
    public class RegisterModel
    {
        [Required]
        public string NameAndSurname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}