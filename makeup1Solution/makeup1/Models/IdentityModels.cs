using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace makeup1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string ProfilePic { get; set; }
        public string ProfilePhoto
        {
            get
            {
                return ProfilePic;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    ProfilePic = "http://i12.photobucket.com/albums/a229/heiddisskoli/default_profile_zpstffrjvqb.jpg";
                }
                else
                {
                    ProfilePic = value;
                }
            }
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Linq.IQueryable<ApplicationUser> UserName { get; set; }
    }

}