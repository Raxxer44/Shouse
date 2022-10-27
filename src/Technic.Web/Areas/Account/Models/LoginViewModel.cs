﻿using System.ComponentModel.DataAnnotations;

namespace Technic.Web.Areas.Account.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
