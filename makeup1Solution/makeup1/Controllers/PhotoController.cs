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

            UsersAccount model = new UsersAccount();
            model.photos = rep.GetUsersPhotos(userId);

            return View(model);
        }
        
        //hér skila ég modeli er í vinnslu
        public ActionResult Categories(string categorie)
        {
            PhotoRepository photoRep = new PhotoRepository();
            CategorieViewModel model = new CategorieViewModel();
            model.catePhotos = photoRep.GetPhotoByCategorie(categorie);

            return View(model);
        }

        public ActionResult FriendsProfile(string id)
        {
            UserRepository userRep = new UserRepository();

            ApplicationUser user = userRep.GetUserByUsername(id);
            string loggedInUser = User.Identity.GetUserName();

            PhotoRepository rep = new PhotoRepository();

            UsersAccount model = new UsersAccount();

            model.username = id;
            model.isFollowing = userRep.IsFollowing(loggedInUser, user.UserName);
            model.photos = rep.GetUsersPhotos(user.Id);

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
        public ActionResult Upload(UploadModel model)
        {
            PhotoRepository repo = new PhotoRepository();
            model.userid = User.Identity.GetUserId();
            bool resp = repo.AddPhoto(model);

            return RedirectToAction("MyProfile", "Photo");
        }
	}
}