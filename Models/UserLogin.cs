using System;
using System.Collections.Generic;

namespace MovieData.Models
{
    public partial class UserLogin
    {
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
