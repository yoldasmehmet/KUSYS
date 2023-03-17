﻿using System.ComponentModel.DataAnnotations;
namespace Library.Security.Models
{
    public class UserModel
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string ApplicationName { get; set; }
    }
}