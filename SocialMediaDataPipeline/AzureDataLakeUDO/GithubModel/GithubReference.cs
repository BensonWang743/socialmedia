
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GithubReference
    {
        //
        // Summary:
        //     The URL associated with this reference.
        public string Url { get; set; }
        //
        // Summary:
        //     The reference label.
        public string Label { get; set; }
        //
        // Summary:
        //     The reference identifier.
        public string Ref { get; set; }
        //
        // Summary:
        //     The sha value of the reference.
        public string Sha { get; set; }
        //
        // Summary:
        //     The user associated with this reference.
        public GitUser User { get; set; }
        //
        // Summary:
        //     The repository associated with this reference.
        public GitRepository Repository { get; set; }
    }
}
