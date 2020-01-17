using PagedList;
using RagnarokWebsite.Constants;
using RagnarokWebsite.Controllers;
using RagnarokWebsite.Data.Context;
using RagnarokWebsite.Data.Models;
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
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            int pageSize = (pageLength ?? 25);
            int pageNumber = (page ?? 1);
            int pageYear = (year ?? 2020);

            CampsViewModel cvm = new CampsViewModel();
            var camps = db.Camps.Where(c => c.Year == pageYear).Select(camp => new CampViewModel
            {
                CampId = camp.CampId,
                CampMasterId = camp.CampMasterId,
                Name = camp.Name,
                Location = camp.Location,
                Year = camp.Year.ToString(),
                CMCharacterName = camp.CampMaster.CharacterName, 
                NumRegistrations = camp.Participants.Where(p => p.Status == "Paid" || p.Status == "Checked In").Count()
            }).OrderBy(c => c.Name);

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

        // GET: Administration/Camps/Create
        public ActionResult Create()
        {
            if (!ValidateCredentials(PermissionConstants.CREATE_NEW_CAMPS))
            {
                return RedirectToAction("Index");
            }

            return View(new CampViewModel());
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            if (!ValidateCredentials(PermissionConstants.CREATE_NEW_CAMPS))
            {
                return RedirectToAction("Index");
            }

            try
            {
                Camp newCamp = new Camp
                {
                    CampId = 0, 
                    Name = formCollection["Name"], 
                    Location = formCollection["Location"], 
                    Year = Convert.ToInt16(formCollection["Year"]), 
                    CampMaster = new CampMaster
                    {
                        CampMasterId = 0, 
                        FirstName = formCollection["CMFirstName"], 
                        LastName = formCollection["CMLastName"], 
                        CharacterName = formCollection["CMCharacterName"], 
                        Email = formCollection["CMEmail"]
                    }
                };

                db.Camps.Add(newCamp);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult Edit(int id)
        {
            if (!ValidateCredentials(PermissionConstants.EDIT_EXISTING_CAMPS))
            {
                return RedirectToAction("Index");
            }

            var camp = db.Camps.FirstOrDefault(c => c.CampId == id);
            if (camp == null)
            {
                return View("Error");
            }

            return View(new CampViewModel(camp));
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            if (!ValidateCredentials(PermissionConstants.EDIT_EXISTING_CAMPS))
            {
                return RedirectToAction("Index");
            }

            try
            {
                var userId = GetCurrentUserId();

                int existingCampId = Convert.ToInt32(formCollection["CampId"]);
                Camp camp = db.Camps.FirstOrDefault(c => c.CampId == existingCampId);
                if (camp == null)
                {
                    return View("Error");
                }

                camp.Name = formCollection["Name"];
                camp.Location = formCollection["Location"];
                camp.Year = Convert.ToInt16(formCollection["Year"]);
                camp.CampMaster.FirstName = formCollection["CMFirstName"];
                camp.CampMaster.LastName = formCollection["CMLastName"];
                camp.CampMaster.CharacterName = formCollection["CMCharacterName"];
                camp.CampMaster.Email = formCollection["CMEmail"];

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int campId)
        {
            if (!ValidateCredentials(PermissionConstants.DELETE_CAMPS))
            {
                return RedirectToAction("Index");
            }

            Camp camp = db.Camps.FirstOrDefault(c => c.CampId == campId);
            if (User != null)
            {
                db.Camps.Remove(camp);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult GenerateNumbers()
        {
            if (!ValidateCredentials(PermissionConstants.VIEW_CAMP_ADMINISTRATION))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            var camps = db.Camps.Where(c => c.Year == YearConstants.CURRENT_EVENT_YEAR).Select(camp => new CampViewModel
            {
                CampId = camp.CampId,
                CampMasterId = camp.CampMasterId,
                Name = camp.Name,
                Location = camp.Location,
                Year = camp.Year.ToString(),
                CMCharacterName = camp.CampMaster.CharacterName,
                NumRegistrations = camp.Participants.Where(p => p.Status == "Paid" || p.Status == "Checked In").Count()
            }).OrderBy(c => c.Name).ToList();

            return View(camps);
        }
    }
}