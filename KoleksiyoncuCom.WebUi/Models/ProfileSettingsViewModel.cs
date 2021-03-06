﻿using KoleksiyoncuCom.Entities;
using System.ComponentModel.DataAnnotations;

namespace KoleksiyoncuCom.WebUi.Models
{
    public class ProfileSettingsViewModel
    {
        public int BuyerId { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string BuyerName { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string ProfileImageUrl { get; set; }
    }
}