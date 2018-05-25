using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaConsole
{
    public class GithubPlan
    {
        //
        // Summary:
        //     The number of collaborators allowed with this plan.
        //
        // Remarks:
        //     This returns System.Int64 because GitHub Enterprise uses a sentinel value of
        //     999999999999 to denote an "unlimited" number of collaborators.
        public long Collaborators { get;  set; }
        //
        // Summary:
        //     The name of the plan.
        public string Name { get;  set; }
        //
        // Summary:
        //     The number of private repositories allowed with this plan.
        //
        // Remarks:
        //     This returns System.Int64 because GitHub Enterprise uses a sentinel value of
        //     999999999999 to denote an "unlimited" number of plans.
        public long PrivateRepos { get;  set; }
        //
        // Summary:
        //     The amount of disk space allowed with this plan.
        //
        // Remarks:
        //     This returns System.Int64 because GitHub Enterprise uses a sentinel value of
        //     999999999999 to denote an "unlimited" amount of disk space.
        public long Space { get;  set; }
        //
        // Summary:
        //     The billing email for the organization. Only has a value in response to editing
        //     an organization.
        public string BillingEmail { get;  set; }
    }
}
