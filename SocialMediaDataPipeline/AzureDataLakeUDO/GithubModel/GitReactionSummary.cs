using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GitReactionSummary
    {
        public int TotalCount { get; set; }
        [Parameter(Key = "+1")]
        public int Plus1 { get; set; }
        [Parameter(Key = "-1")]
        public int Minus1 { get; set; }
        public int Laugh { get; set; }
        public int Confused { get;set; }
        public int Heart { get; set; }
        public int Hooray { get; set; }
        public string Url { get; set; }
    }
}
