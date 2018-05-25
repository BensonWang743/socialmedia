using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaConsole
{
    public class GithubUser:GithubAccount
    {
        public GithubRepositoryPermissions Permissions { get;  set; }
        //
        // Summary:
        //     Whether or not the user is an administrator of the site
        //public bool SiteAdmin { get;  set; }
        //
        // Summary:
        //     When the user was suspended, if at all (GitHub Enterprise)
        public DateTimeOffset? SuspendedAt { get;  set; }
        //
        // Summary:
        //     Whether or not the user is currently suspended
        public bool aSuspendeded { get; set; }
        //
        // Summary:
        //     LDAP Binding (GitHub Enterprise only)

        public string LdapDistinguishedName { get;  set; }
        //
        // Summary:
        //     Date the user account was updated.
        public DateTimeOffset UpdatedAt { get;  set; }
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }
        public string d { get; set; }
    }
}
