using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace MovieData.Models
{
    public class RegisterRequestModel:IRequest<RegisterResponseModel>
    {
        [Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        [DataType(DataType.EmailAddress)]
        //[Remote("doesUserNameExist", "Home", HttpMethod = "POST", ErrorMessage = "Email already exists. Please enter a different Email.")]

        public string EmailID { get; set; }


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

    }
    public class RegisterResponseModel
    {
        public bool Success { get; set; }

    }
    internal class RegistrationHandler : IRequestHandler<RegisterRequestModel, RegisterResponseModel>
    {
        IUserDataAcess _userDataAcess;
       private readonly IMapper _mapper;
        //Constructor Injection
        public RegistrationHandler(IMapper mapper,IUserDataAcess userDataAcess)
        {
           _userDataAcess = userDataAcess;
            _mapper = mapper;
        }
        public async Task<RegisterResponseModel> Handle(RegisterRequestModel request, CancellationToken cancellationToken)
        {
            return new RegisterResponseModel
            {
                Success = _userDataAcess.addUser(_mapper.Map<User>(request))
            };
        }
    }
}
