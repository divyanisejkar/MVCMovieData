using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MovieData.Models
{
    public class RequestModel : IRequest<ResponseModel>
    {
        public string EmailID { get; set; }

        public string Password { get; set; }
    }

    public class ResponseModel
    {
        public bool Success { get; set; }
        public string ResponseText { get; set; }
    }

    internal class LoginHandler : IRequestHandler<RequestModel, ResponseModel>
    {
        public LoginHandler()
        {

        }

        public async Task<ResponseModel> Handle(RequestModel request, CancellationToken cancellationToken)
        {
            UserDataAccess userDataAccess = new UserDataAccess();
            
            bool success = userDataAccess.CheckUserLogin(request.EmailID,request.Password);
            return new ResponseModel() { Success = success, ResponseText = "Login Successfull" };
           
        }
    }
}

