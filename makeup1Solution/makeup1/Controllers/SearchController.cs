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
        public JsonResult SearchForUser(string query)
        {
            UserRepository rep = new UserRepository();
            List<UserViewModel> users = rep.Search(query);
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }
	}
}