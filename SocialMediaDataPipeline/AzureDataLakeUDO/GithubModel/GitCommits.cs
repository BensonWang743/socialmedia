using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GitCommits:GithubReference
    {
        public string Message { get;  set; }
        public GitCommitter Author { get;  set; }
        public GitCommitter Committer { get;  set; }
        public GithubReference Tree { get;  set; }
        public IReadOnlyList<GithubReference> Parents { get;  set; }
        public int CommentCount { get;  set; }
        public GitVerification Verification { get;  set; }
    }
}
