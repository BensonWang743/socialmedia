using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GitUser:GitAccount
    {
        public GitRepositoryPermissions Permissions { get; set; }
        //
        // Summary:
        //     Whether or not the user is an administrator of the site
        public bool SiteAdmin { get;  set; }
        //
        // Summary:
        //     When the user was suspended, if at all (GitHub Enterprise)
        public DateTimeOffset? SuspendedAt { get; set; }
        //
        // Summary:
        //     Whether or not the user is currently suspended
        public bool Suspended { get; set; }
        //
        // Summary:
        //     LDAP Binding (GitHub Enterprise only)

        public string LdapDistinguishedName { get; set; }
        //
        // Summary:
        //     Date the user account was updated.
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
