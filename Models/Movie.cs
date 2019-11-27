﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MovieData.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Genre { get; set; }

        [Required]
        [Range(1900,2020)]
        public string ReleseDate { get; set; }
        
    }
}