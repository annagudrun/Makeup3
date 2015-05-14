using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using makeup1.Models;
using makeup1.Repositories;
using Microsoft.AspNet.Identity;
using makeup1.ViewModels;

namespace makeup1.Controllers
{
    public class PhotoController : Controller
    {
        IPhotoRepository photoRepository;


        public PhotoController()
        {
            photoRepository = new PhotoRepository();
        }

        // Test constructor, takes a repository as argument.
        public PhotoController(IPhotoRepository photoRepo)
        {
            photoRepository = photoRepo;
        }

        public ActionResult MyProfile()
        {
            string userId = User.Identity.GetUserId();

            PhotoRepository rep = new PhotoRepository();

            UserAccountViewModel model = new UserAccountViewModel();
            model.Photos = rep.GetUsersPhotos(userId);
            model.User = rep.GetUserByID(User.Identity.GetUserId());


            return View(model);
        }
        
     
        public ActionResult FriendsProfile(string id)
        {
            UserRepository userRep = new UserRepository();

            ApplicationUser user = userRep.GetUserByUsername(id);
            string loggedInUser = User.Identity.GetUserName();

            PhotoRepository rep = new PhotoRepository();

            UserAccountViewModel model = new UserAccountViewModel();

            model.Username = id;
            model.IsFollowing = userRep.IsFollowing(loggedInUser, user.UserName);
            model.Photos = rep.GetUsersPhotos(user.Id);

            return View("MyProfile", model);
        }

        public JsonResult FollowUser(string username)
        {
            PhotoRepository repo = new PhotoRepository();
            string user = User.Identity.GetUserName();
            bool resp = repo.FollowUser(user, username);

           
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UnFollowUser(string username)
        {
            PhotoRepository repo = new PhotoRepository();
            string user = User.Identity.GetUserName();
            bool resp = repo.UnFollowUser(user, username);

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Upload(UploadViewModel model)
        {
            PhotoRepository repo = new PhotoRepository();
            model.Userid = User.Identity.GetUserId();
            bool resp = repo.AddPhoto(model);

            return Json(new { success = true });//RedirectToAction("MyProfile", "Photo");
        }
	}
}