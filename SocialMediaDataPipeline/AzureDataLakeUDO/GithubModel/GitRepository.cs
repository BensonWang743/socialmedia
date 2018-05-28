using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GitRepository
    {
        public int OpenIssuesCount { get; set; }
        public DateTimeOffset? PushedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public GitRepositoryPermissions Permissions { get; set; }
        public GitRepository Parent { get; set; }
        public GitRepository Source { get; set; }
        public bool HasIssues { get; set; }
        public string DefaultBranch { get; set; }
        public bool HasWiki { get; set; }
        public bool HasDownloads { get; set; }
        public bool? AllowRebaseMerge { get; set; }
        public bool? AllowSquashMerge { get; set; }
        public bool? AllowMergeCommit { get; set; }
        public bool HasPages { get; set; }
        public GitLicenseMetadata License { get; set; }
        public int StargazersCount { get; set; }
        public int ForksCount { get; set; }
        public bool Fork { get; set; }
        public string Url { get; set; }
        public string HtmlUrl { get; set; }
        public string CloneUrl { get; set; }
        public string GitUrl { get; set; }
        public string SshUrl { get; set; }
        public string SvnUrl { get; set; }
        public string MirrorUrl { get; set; }
        public long Id { get; set; }
        public GitUser Owner { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Homepage { get; set; }
        public string Language { get; set; }
        public bool Private { get; set; }
        public int SubscribersCount { get; set; }
        public long Size { get; set; }
    }
}
