using makeup1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace makeup1.ViewModels
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public string ProfilePhoto { get; set; }

        public UserViewModel()
        {

        }

        public UserViewModel(ApplicationUser user)
        {
            Username = user.UserName;
            ProfilePhoto = user.ProfilePic;
        }
    }
}