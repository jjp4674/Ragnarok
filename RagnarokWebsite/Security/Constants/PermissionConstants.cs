using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RagnarokWebsite.Security.Constants
{
    public static class PermissionConstants
    {
        // User Administration
        public const string VIEW_USER_ADMINISTRATION = "View User Administration";
        public const string CREATE_NEW_USERS = "Create New Users";
        public const string EDIT_EXISTING_USERS = "Edit Existing Users";
        public const string DELETE_USERS = "Delete Users";
        public const string LIMIT_VIEWABLE_CALLS = "Limit Viewable Calls";

        // Role Administration
        public const string VIEW_ROLE_ADMINISTRATION = "View Role Administration";
        public const string CREATE_NEW_ROLES = "Create New Roles";
        public const string EDIT_EXISTING_ROLES = "Edit Existing Roles";
        public const string DELETE_ROLES = "Delete Roles";

        // Camp Administration
        public const string VIEW_CAMP_ADMINISTRATION = "View Camp Administration";
        public const string CREATE_NEW_CAMPS = "Create New Camps";
        public const string EDIT_EXISTING_CAMPS = "Edit Existing Camps";
        public const string DELETE_CAMPS = "Delete Camps";

        // Attendee Administration
        public const string VIEW_ATTENDEE_ADMINISTRATION = "View Attendee Administration";
        public const string CREATE_NEW_ATTENDEES = "Create New Attendees";
        public const string EDIT_EXISTING_ATTENDEES = "Edit Existing Attendees";
        public const string DELETE_ATTENDEES = "Delete Attendees";

        // Attendee Check-In
        public const string VIEW_ATTENDEE_CHECKIN = "View Attendee Check-In";
    }
}