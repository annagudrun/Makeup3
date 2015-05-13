using makeup1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace makeup1.ViewModels
{
    public class UserViewModel
    {
        public string  username { get; set; }
        public string profilePhoto { get; set; }
        public UserViewModel()
        {

        }
        public UserViewModel(ApplicationUser user)
        {
            username = user.UserName;
            profilePhoto = user.PasswordHash;
        }
    }
}