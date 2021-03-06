﻿using PagedList;
using RagnarokWebsite.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RagnarokWebsite.Models
{
    public class CampsViewModel
    {
        public int Page { get; set; }
        public int PageLength { get; set; }

        public List<SelectListItem> Options { get; set; }
        public string Year { get; set; }

        public IPagedList<CampViewModel> Camps { get; set; }

        public CampsViewModel()
        {
            Options = DateExtensions.GetDateOptions();
        }
    }
}