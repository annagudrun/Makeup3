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
        public ActionResult Categories(string categorie)
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