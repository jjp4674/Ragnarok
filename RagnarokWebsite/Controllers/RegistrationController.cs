using RagnarokWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RagnarokWebsite.Controllers
{
    [AllowAnonymous]
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View(new RegistrationViewModel());
        }
    }
}