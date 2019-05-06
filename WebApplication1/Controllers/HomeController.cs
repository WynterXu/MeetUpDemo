using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        string url = "https://secure.meetup.com/oauth2/authorize?client_id=uam3aph3b62gvn3qq3rlubmldr&response_type=code&redirect_uri=http://localhost:1294/Values/Code";
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return Redirect(url);

            //return View();
        }

    }
}
