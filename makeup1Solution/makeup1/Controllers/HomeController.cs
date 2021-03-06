﻿using makeup1.Repositories;
using makeup1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace makeup1.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            PhotoRepository repo = new PhotoRepository();
            string user = User.Identity.GetUserName();

            NewsFeedViewModel model = new NewsFeedViewModel();
            model.NewsFeedPhotos = repo.GetFollowersPhotos(user);
            return View(model);
        }

        public ActionResult EditProfile()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Artist()
        {
            return View();
        }

        public ActionResult Offer()
        {
            return View();
        }

        public ActionResult MyProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(FormCollection collection)
        {
            string userID = User.Identity.GetUserId();
            string profilePicture = collection["profilePicture"];

            UserRepository.Instance.editPicture(userID, profilePicture);

            return RedirectToAction("Index");
        }
       
    }
}