using PagedList;
using RagnarokWebsite.Controllers;
using RagnarokWebsite.Data.Context;
using RagnarokWebsite.Models;
using RagnarokWebsite.Security.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RagnarokWebsite.Areas.Administration.Controllers
{
    [Authorize]
    public class CampsController : RagnarokController
    {
        private RagnarokContext db = new RagnarokContext();

        // GET: Administration/Camps
        public ActionResult Index(int? page, int? pageLength, int? year)
        {
            if (!ValidateCredentials(PermissionConstants.VIEW_CAMP_ADMINISTRATION))
            {
                return RedirectToAction("Index", "Dashboard", new { area = "" });
            }

            int pageSize = (pageLength ?? 25);
            int pageNumber = (page ?? 1);
            int pageYear = (year ?? 2020);
            var camps = db.Camps.AsQueryable();

            CampsViewModel cvm = new CampsViewModel();
            camps = camps.Where(c => c.Year == pageYear).OrderBy(c => c.Name);
            cvm.Camps = camps.ToPagedList(pageNumber, pageSize);
            cvm.Page = pageNumber;

            if (cvm.Camps.PageNumber > cvm.Camps.PageCount)
            {
                cvm.Camps = camps.ToPagedList(1, pageSize);
                cvm.Page = 1;
            }

            cvm.PageLength = pageSize;
            cvm.Year = pageYear.ToString();

            return View(cvm);
        }
    }
}