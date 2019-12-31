using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;

namespace RagnarokWebsite.Controllers
{
    public class LogoutController : RagnarokController
    {
        public ActionResult Index()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }
    }
}