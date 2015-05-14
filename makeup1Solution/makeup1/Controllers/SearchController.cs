using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using makeup1.Repositories;
using makeup1.Models;
using makeup1.ViewModels;

namespace makeup1.Controllers
{
    public class SearchController : Controller
    {
        PhotoRepository photoRepo = new PhotoRepository();

        public JsonResult SearchForUser(string query)
        {
            UserRepository rep = new UserRepository();
            List<UserViewModel> users = rep.Search(query);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchForHashtag(string hashtag)
        {
            IEnumerable<Photo> photos = photoRepo.GetPhotosByHashtag(hashtag.ToLower());

            List<Object> result = new List<Object>();

            foreach (Photo photo in photos)
            {
                result.Add(new
                {
                    Caption = photo.Caption,
                    ID = photo.ID,
                    PhotoUrl = photo.photoUrl
                });
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }
	}
}