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
        // Hann ætti að hafa photo repository 
        IPhotoRepository photoRepository;
        //PhotoRepository repo = new PhotoRepository();


        public PhotoController()
        {
            photoRepository = new PhotoRepository();
        }

        // Test constructor, takes a repository as argument.
        public PhotoController(IPhotoRepository photoRepo)
        {
            photoRepository = photoRepo;
        }

            
        //The parameter is the user id, then we will find all
        //photos with the same user id
        public ActionResult MyProfile()
        {
            //PhotoViewModel viewModel = new PhotoViewModel();

            string userId = User.Identity.GetUserId();

            PhotoRepository rep = new PhotoRepository();

            UsersAccount model = new UsersAccount();
            model.photos = rep.GetUsersPhotos(userId);
            
           // Console.WriteLine(viewModel);
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
            //PhotoViewModel viewModel = new PhotoViewModel();

            UserRepository userRep = new UserRepository();

            ApplicationUser user = userRep.GetUserByUsername(id);
            string loggedInUser = User.Identity.GetUserName();

            PhotoRepository rep = new PhotoRepository();

            UsersAccount model = new UsersAccount();

            model.username = id;
            model.isFollowing = userRep.IsFollowing(loggedInUser, user.UserName);
            model.photos = rep.GetUsersPhotos(user.Id);

            // Console.WriteLine(viewModel);
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

/*
        [HttpPost]
        public ActionResult AddPhoto(string photoUrl, string caption)
        {
            // test if parameters are correct
            string userId = User.Identity.GetUserId();
            var p = new Photo { photoUrl = photoUrl, Caption = caption , DateCreated = DateTime.Now, UserId = userId };
            photoRepository.Add(p);
           // var model = photoRepository.Add(new Photo(photoUrl,comment,userId));

            return RedirectToAction("MyProfile", "Photo");
        } 
*/

        // GET: /Photo/
        /*
        public ActionResult Index()
        {
            List<Photo> mockPhotos = new List<Photo>();

            Photo photo1 = new Photo("http://www.pictures4cool.com/media/images/picture-wallpaper.jpg");
            photo1.ID = 0;
            
            Photo photo2 = new Photo("http://globe-views.com/dcim/dreams/photo/photo-06.jpg");
            photo2.ID = 1;
           
            mockPhotos.Add(photo1);
            mockPhotos.Add(photo2);

            return View(mockPhotos);
        }*/
	}
}