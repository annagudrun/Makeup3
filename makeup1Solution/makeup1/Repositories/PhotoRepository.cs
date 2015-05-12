using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using makeup1.Models;
using makeup1.ViewModels;

namespace makeup1.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Photo> GetAllPhotos
        {
            get { return db.Photos; }
            set { }
        }

        public bool Add(Photo photo)
        {
            db.Photos.Add(photo);

            db.SaveChanges();  

            return true;
        }


        public List<Photo> GetUsersPhotos(string userId)
        {
            return db.Photos.Where(a => a.UserId == userId).ToList();
        }
        //her getum vid verid med delete ef vid viljum

        public bool FollowUser(string user, string username)
        {
            try
            {
                db.Followers.Add(new Follower()
                   {
                       FollowerName = user,
                       FollowerUserId = username
                   });
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal bool UnFollowUser(string user, string username)
        {
            try
            {
                Follower follower = db.Followers.FirstOrDefault(a => a.FollowerName == user && a.FollowerUserId == username);
                db.Followers.Remove(follower);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

      /*  internal List <Hashtag> GetTagsForPhoto(int photoId)
        {
            return db.Hashtags.Where(a => a.HashtagPhotoId
        }*/

        internal List<Photo> GetFollowersPhotos(string username)
        {
            List<string> followingUser = db.Followers.Where(a => a.FollowerName == username).Select(b => b.FollowerUserId).ToList();

            List<string> userIds = db.Users.Where(a => followingUser.Contains(a.UserName)).Select(b => b.Id).ToList();

            return db.Photos.Where(a => userIds.Contains(a.UserId)).ToList();
        }

        internal bool AddPhoto(UploadModel model)
        {
            // hér væri ég til í að setja hashtagPhotoId = Photo_ID
            try
            {
                List<string> hashTags = model.hash.Split(' ').ToList();
                List<Hashtag> tags = new List<Hashtag>();
                foreach (var t in hashTags)
                {
                    tags.Add(new Hashtag()
                    {
                        HastagName = t
                    });
                }

                Photo ph = new Photo()
                {
                    photoUrl = model.imageUrl,
                    Caption = model.caption,
                    DateCreated = DateTime.Now,
                    UserId = model.userid,
                    Hashtags = tags
                };

                db.Photos.Add(ph);

                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}