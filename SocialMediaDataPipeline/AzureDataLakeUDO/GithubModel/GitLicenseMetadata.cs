using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GitLicenseMetadata
    {
        //
        // Summary:
        //     The
        public string Key { get; set; }
        //
        // Summary:
        //     Friendly name of the license.
        public string Name { get; set; }
        //
        // Summary:
        //     SPDX license identifier.
        public string SpdxId { get; set; }
        //
        // Summary:
        //     URL to retrieve details about a license.
        public string Url { get; set; }
        //
        // Summary:
        //     Whether the license is one of the licenses featured on https://choosealicense.com
        public bool Featured { get; set; }
    }
}
