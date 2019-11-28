using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace RagnarokWebsite.Extensions
{
    public static class LinkExtensions
    {
        public static HtmlString NavigationLink(this HtmlHelper html, string linkText, string actionName, string controllerName)
        {
            string contextAction = (string)html.ViewContext.RouteData.Values["action"];
            string contextController = (string)html.ViewContext.RouteData.Values["controller"];

            bool isCurrent =
                string.Equals(contextAction, actionName, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(contextController, controllerName, StringComparison.CurrentCultureIgnoreCase);

            return html.ActionLink(
                linkText,
                actionName,
                controllerName,
                new { area = "" },
                htmlAttributes: isCurrent ? new { @class = "nav-link current" } : new { @class = "nav-link" }
                );
        }

        public static string DropdownToggleCurrent(this HtmlHelper html, string controllerName)
        {
            string contextController = (string)html.ViewContext.RouteData.Values["controller"];

            bool isCurrent =
                string.Equals(contextController, controllerName, StringComparison.CurrentCultureIgnoreCase);

            return isCurrent ? " current" : "";
        }

        public static HtmlString DropdownLink(this HtmlHelper html, string linkText, string actionName, string controllerName)
        {
            string contextAction = (string)html.ViewContext.RouteData.Values["action"];
            string contextController = (string)html.ViewContext.RouteData.Values["controller"];

            bool isCurrent =
                string.Equals(contextAction, actionName, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(contextController, controllerName, StringComparison.CurrentCultureIgnoreCase);

            return html.ActionLink(
                linkText,
                actionName,
                controllerName,
                new { area = "" },
                htmlAttributes: isCurrent ? new { @class = "dropdown-item current" } : new { @class = "dropdown-item" }
                );
        }
    }
}