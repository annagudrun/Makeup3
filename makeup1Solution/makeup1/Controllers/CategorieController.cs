using makeup1.Models;
using makeup1.Repositories;
using makeup1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace makeup1.Controllers
{
    public class CategorieController : Controller
    {
       public ActionResult EveningMakeup()
        {
            return RedirectToAction("CategorieList", new { categorie = "Kvöldförðun" });
        }

       public ActionResult DayMakeup()
       {
           return RedirectToAction("CategorieList", new { categorie = "Dagsförðun" });
       }

       public ActionResult MakeupProduct()
       {
           return RedirectToAction("CategorieList", new { categorie = "Snyrtivörur" });
       }

       public ActionResult MakeupByMe()
       {
           return RedirectToAction("CategorieList", new { categorie = "Förðun eftir mig" });
       }

        public ActionResult CategorieList(string categorie)
        {
            PhotoRepository photoRep = new PhotoRepository();
            CategorieViewModel model = new CategorieViewModel();
            model.catePhotos = photoRep.GetPhotoByCategorie(categorie);
            return View(model);
        }

        //
        // GET: /Categorie/
        public ActionResult Index()
        {
            return View();
        }
	}
}