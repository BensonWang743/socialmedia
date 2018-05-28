using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GitRepositoryPermissions
    {
        //
        // Summary:
        //     Whether the current user has administrative permissions
        public bool Admin { get; set; }
        //
        // Summary:
        //     Whether the current user has push permissions
        public bool Push { get; set; }
        //
        // Summary:
        //     Whether the current user has pull permissions
        public bool Pull { get; set; }
    }
}
