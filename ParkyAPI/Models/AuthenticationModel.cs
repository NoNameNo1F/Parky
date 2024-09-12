using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ParkyAPI.Models
{
    public class AuthenticationModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}