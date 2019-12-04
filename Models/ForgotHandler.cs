using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MovieData.Models
{

    public class ForgotRequestModel :IRequest<ForgotResponseModel>
    {
        [Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        [DataType(DataType.EmailAddress)]
        //[Remote("doesUserNameExist", "Home", HttpMethod = "POST", ErrorMessage = "Email already exists. Please enter a different Email.")
        public string EmailID { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
      
        public string new_pwd { get; set; }
    }
    public class ForgotResponseModel
    {

        public bool Success { get; set; }
       // public string ResponseText { get; set; }

    }
    internal class ForgotHandler : IRequestHandler<ForgotRequestModel, ForgotResponseModel>
    {
        public async Task<ForgotResponseModel> Handle(ForgotRequestModel request, CancellationToken cancellationToken)
        {
            UserDataAccess userDataAccess = new UserDataAccess();
            bool success = userDataAccess.NewPassword(request.EmailID, request.new_pwd);
            return new ForgotResponseModel() { Success = success };

        }
    }
}
