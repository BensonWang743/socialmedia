using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaConsole
{
    public class GithubRepositoryPermissions
    {
        //
        // Summary:
        //     Whether the current user has administrative permissions
        public bool Admin { get; protected set; }
        //
        // Summary:
        //     Whether the current user has push permissions
        public bool Push { get; protected set; }
        //
        // Summary:
        //     Whether the current user has pull permissions
        public bool Pull { get; protected set; }
    }
}
