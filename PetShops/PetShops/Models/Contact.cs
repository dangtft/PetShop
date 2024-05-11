﻿using System.ComponentModel.DataAnnotations;

namespace PetShops.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The Email field is required.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "The Message field is required.")]
        public string? Message { get; set; }
    }
}