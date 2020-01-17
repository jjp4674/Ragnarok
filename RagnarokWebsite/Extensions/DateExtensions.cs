using System.Collections.Generic;
using System.Web.Mvc;

namespace RagnarokWebsite.Extensions
{
    public static class DateExtensions
    {
        public static List<SelectListItem> GetDateOptions()
        {
            List<SelectListItem> options = new List<SelectListItem>();
            options.Add(new SelectListItem
            {
                Value = "2017",
                Text = "2017"
            });
            options.Add(new SelectListItem
            {
                Value = "2018",
                Text = "2018"
            });
            options.Add(new SelectListItem
            {
                Value = "2019",
                Text = "2019"
            });
            options.Add(new SelectListItem
            {
                Value = "2020",
                Text = "2020",
                Selected = true
            });

            return options;
        }
    }
}