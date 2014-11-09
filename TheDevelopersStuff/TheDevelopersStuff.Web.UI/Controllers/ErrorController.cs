using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheDevelopersStuff.Web.UI.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Err/

        public ActionResult Index()
        {
            return View("Error");
        }

    }
}
