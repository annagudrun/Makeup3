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

        public List<Photo> GetPhotoByCategorie(string categorie)
        {
            return db.Photos.Where(a => a.Categorie == categorie).OrderByDescending(a => a.DateCreated).ToList();
        }

        public List<Photo> GetUsersPhotos(string userId)
        {
            return db.Photos.Where(a => a.UserId == userId).OrderByDescending(a => a.DateCreated).ToList();
        }


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

        public bool UnFollowUser(string user, string username)
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

        public ApplicationUser GetUserByID(string userID)
        {
            ApplicationUser user = (from s in db.Users
                                    where s.Id == userID
                                    select s).SingleOrDefault();
            return user;
        }


        public List<Photo> GetFollowersPhotos(string username)
        {
            List<string> followingUser = db.Followers.Where(a => a.FollowerName == username).Select(b => b.FollowerUserId).ToList();

            List<string> userIds = db.Users.Where(a => followingUser.Contains(a.UserName)).Select(b => b.Id).ToList();

            return db.Photos.Where(a => userIds.Contains(a.UserId)).OrderByDescending(a => a.DateCreated).ToList();
        }

        public bool AddPhoto(UploadViewModel model)
        {
            try
            {
                List<string> hashTags = model.Hash.Split(' ').ToList();
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
                    photoUrl = model.ImageUrl,
                    Caption = model.Caption,
                    DateCreated = DateTime.Now,
                    UserId = model.Userid,
                    Categorie = model.Categorie,
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

        public IEnumerable<Photo> GetPhotosByHashtag(string tag)
        {
            IEnumerable<int> photoIds = (from hashtag in db.Hashtags
                                         where hashtag.HastagName == tag
                                          select hashtag.HashtagPhotoId).ToList();

        
            IEnumerable<Photo> photos = (from photo in db.Photos
                                         where photoIds.Contains(photo.ID)
                                         select photo).ToList();

            return photos;
        }
    }
}