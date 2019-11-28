using System;
using System.Collections.Generic;

namespace MovieData.Models
{
    public partial class MvcMovieContext
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string ReleseDate { get; set; }
    }
}
