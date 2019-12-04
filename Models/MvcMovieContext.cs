using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieData.Models
{
    public partial class MvcMovieContext
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Movie Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter Movie Genre")]
        public string Genre { get; set; }

        [Range(1990, 2060)]
        
        public string ReleseDate { get; set; }
    }
}
