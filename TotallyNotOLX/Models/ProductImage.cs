﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TotallyNotOLX.Models
{
    public class ProductImage
    {
        /// <summary>
        /// The properties of ProductImage table of our database.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        public int ProductId { get; set; }

        public string Description { get; set; }

        public Product Product { get; set; }
    }
}
