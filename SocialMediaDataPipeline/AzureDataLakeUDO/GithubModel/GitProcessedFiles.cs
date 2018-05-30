using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GitProcessedFiles
    {
        public string Status { get; set; }
        public string RawUrl { get; set; }
        public string PreviousFileName { get; set; }
    }
}
