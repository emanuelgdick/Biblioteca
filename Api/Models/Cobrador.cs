﻿using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Cobrador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ApeyNom { get; set; }
    }
}
