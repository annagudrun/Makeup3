using makeup1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace makeup1.ViewModels
{
    public class UserAccountViewModel
    {     
            public ApplicationUser User { get; set; }
            public List<Photo> Photos { get; set; }
            public string Username { get; set; }
            public bool IsFollowing { get; set; }
    }

}