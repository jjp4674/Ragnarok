using PagedList;
using RagnarokWebsite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RagnarokWebsite.Models
{
    public class CampsViewModel
    {
        public int Page { get; set; }
        public int PageLength { get; set; }

        public List<SelectListItem> Options { get; set; }
        public string Year { get; set; }

        public IPagedList<Camp> Camps { get; set; }

        public CampsViewModel()
        {
            Options = new List<SelectListItem>();

            Options.Add(new System.Web.Mvc.SelectListItem
            {
                Value = "2017",
                Text = "2017"
            });
            Options.Add(new System.Web.Mvc.SelectListItem
            {
                Value = "2018",
                Text = "2018"
            });
            Options.Add(new System.Web.Mvc.SelectListItem
            {
                Value = "2019",
                Text = "2019"
            });
            Options.Add(new System.Web.Mvc.SelectListItem
            {
                Value = "2020",
                Text = "2020",
                Selected = true
            });
        }
    }
}