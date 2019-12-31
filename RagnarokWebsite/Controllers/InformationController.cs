using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RagnarokWebsite.Controllers
{
    [AllowAnonymous]
    public class InformationController : Controller
    {
        // GET: Information
        public ActionResult Pricing()
        {
            return View();
        }

        public ActionResult EventRules()
        {
            return View();
        }

        public ActionResult CodeOfConduct()
        {
            return View();
        }

        public ActionResult MerchantInformation()
        {
            return View();
        }

        public ActionResult CampMasters()
        {
            return View();
        }

        public ActionResult LandGrab()
        {
            return View();
        }

        public ActionResult Map()
        {
            return View();
        }

        public ActionResult MerchantList()
        {
            return View();
        }
    }
}