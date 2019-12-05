using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MovieData.Models
{
    public class User
    {

        [Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        [DataType(DataType.EmailAddress)]
        //[Remote("doesUserNameExist", "Home", HttpMethod = "POST", ErrorMessage = "Email already exists. Please enter a different Email.")]

        public string EmailID { get; set; }

        [PersonalData]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name required")]
        

        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string LastName { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        //[DataType(DataType.Password)]
        //[MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        //[Compare("old_pwd", ErrorMessage = "old and new Password must match.")]

        public string new_pwd{get; set;}

        //[DataType(DataType.Password)]
        //[MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        public string old_pwd { get; set; }



    }
}
