using PagedList;
using RagnarokWebsite.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RagnarokWebsite.Models
{
    public class AttendeesViewModel
    {
        public int Page { get; set; }
        public int PageLength { get; set; }

        public List<SelectListItem> Options { get; set; }
        public string Year { get; set; }

        public string Search { get; set; }

        public IPagedList<AttendeeViewModel> Attendees { get; set; }
        
        public AttendeesViewModel()
        {
            Options = DateExtensions.GetDateOptions();
        }
    }
}