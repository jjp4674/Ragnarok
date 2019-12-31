using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RagnarokWebsite.Security.Constants
{
    public static class LoginErrorConstants
    {
        public const string LDAP_LOGIN_NOT_ENABLED = "LDAP login not enabled";
        public const string INVALID_USERNAME_OR_PASSWORD = "Invalid username or password";
        public const string ACCOUNT_LOCKED = "Account locked";
    }
}