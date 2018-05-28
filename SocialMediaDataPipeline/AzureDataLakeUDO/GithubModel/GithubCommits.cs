using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GithubCommits:GithubReference
    {
        //
        // 摘要:
        //     Gets the GitHub account information for the commit author. It attempts to match
        //     the email address used in the commit with the email addresses registered with
        //     the GitHub account. If no account corresponds to the commit email, then this
        //     property is null.
        public GitAuthor Author { get;  set; }
        public string CommentsUrl { get;  set; }
        public GitCommits Commit { get;  set; }
        //
        // 摘要:
        //     Gets the GitHub account information for the commit committer. It attempts to
        //     match the email address used in the commit with the email addresses registered
        //     with the GitHub account. If no account corresponds to the commit email, then
        //     this property is null.
        public GitAuthor Committer { get;  set; }
        public string HtmlUrl { get;  set; }
        public GitCommitStats Stats { get;  set; }
        public IReadOnlyList<GithubReference> Parents { get;  set; }
        public IReadOnlyList<GitCommitFile> Files { get;  set; }
    }
}
