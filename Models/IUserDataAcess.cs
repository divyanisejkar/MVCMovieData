using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieData.Models
{
    interface IUserDataAcess
    {
        public bool addUser(User user);

        public User CheckUserDetails(string EmailID);

        public bool CheckUserLogin(string EmailID, string Password);

        public bool CheckPassword(string EmailID, string old_pwd, string new_pwd);

        public bool NewPassword(string EmailID, string new_pwd);
       
    }
}
