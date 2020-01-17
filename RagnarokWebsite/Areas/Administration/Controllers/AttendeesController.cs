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
    public class AttendeesController : RagnarokController
    {
        private RagnarokContext db = new RagnarokContext();

        // GET: Administration/Attendees
        public ActionResult Index(int? page, int? pageLength, int? year, string search = "")
        {
            if (!ValidateCredentials(PermissionConstants.VIEW_ATTENDEE_ADMINISTRATION))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            int pageSize = (pageLength ?? 25);
            int pageNumber = (page ?? 1);
            int pageYear = (year ?? 2020);

            AttendeesViewModel avm = new AttendeesViewModel();
            var attendees = db.Participants.Where(p => p.EventYear.Year == pageYear).Select(participant => new AttendeeViewModel
            {
                ParticipantId = participant.ParticipantId,
                FirstName = participant.FirstName,
                LastName = participant.LastName,
                DOB = participant.DOB,
                IsMinor = participant.IsMinor == "Y" ? true : false,
                CampName = participant.Camp.Name,
                CharacterName = participant.CharacterName,
                Status = participant.Status,
                IsMerchant = participant.IsMerchant == "Y" ? true : false
            });

            if (!string.IsNullOrEmpty(search))
            {
                attendees = attendees.Where(p => p.FirstName.ToLower().Contains(search.ToLower()) || p.LastName.ToLower().Contains(search.ToLower()) || 
                    p.CharacterName.ToLower().Contains(search.ToLower()) || p.CampName.ToLower().Contains(search.ToLower()));
            }

            attendees = attendees.OrderBy(c => c.LastName).ThenBy(c => c.FirstName);

            avm.Attendees = attendees.ToPagedList(pageNumber, pageSize);
            avm.Page = pageNumber;

            if (avm.Attendees.PageNumber > avm.Attendees.PageCount)
            {
                avm.Attendees = attendees.ToPagedList(1, pageSize);
                avm.Page = 1;
            }

            avm.PageLength = pageSize;
            avm.Year = pageYear.ToString();

            return View(avm);
        }
    }
}