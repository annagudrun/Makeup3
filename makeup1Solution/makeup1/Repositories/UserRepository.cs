using makeup1.Models;
using makeup1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace makeup1.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public List<ApplicationUser> GetAllUsers()
        {
            return db.Users.ToList();
        }

        public ApplicationUser GetUserByUsername(string username)
        {
            return db.Users.FirstOrDefault(a => a.UserName == username);
        }

        internal List<UserViewModel> Search(string query)
        {
            List<ApplicationUser> user = db.Users.Where(a => a.UserName.Contains(query)).ToList();
            List<UserViewModel> usersToReturn = new List<UserViewModel>();
            user.ForEach(a => usersToReturn.Add(new UserViewModel(a)));
            return usersToReturn;
        }

        internal bool IsFollowing(string user, string username)
        {
            return db.Followers.FirstOrDefault(a => a.FollowerName == user && a.FollowerUserId == username) != null;
        }
    }
}