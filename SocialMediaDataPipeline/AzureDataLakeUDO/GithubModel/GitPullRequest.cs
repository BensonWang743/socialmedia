using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GitPullRequest
    {
        //
        // Summary:
        //     The user who is assigned the pull request.
        public GitUser Assignee { get; set; }
        //
        // Summary:
        //     The multiple users this pull request is assigned to.
        public IReadOnlyList<GitUser> Assignees { get; set; }
        //
        // Summary:
        //     The milestone, if any, that this pull request is assigned to.
        public GitMilestone Milestone { get; set; }
        //
        // Summary:
        //     Whether or not the pull request has been merged.
        public bool Merged { get; }
        //
        // Summary:
        //     Whether or not the pull request can be merged.
        public bool? Mergeable { get; set; }
        //
        // Summary:
        //     Provides extra information regarding the mergeability of the pull request.
        public GitItemState MergeableState { get; set; }
        //
        // Summary:
        //     The value of this field changes depending on the state of the pull request. Not
        //     Merged - the hash of the test commit used to determine mergability. Merged with
        //     merge commit - the hash of said merge commit. Merged via squashing - the hash
        //     of the squashed commit added to the base branch. Merged via rebase - the hash
        //     of the commit that the base branch was updated to.
        public string MergeCommitSha { get; set; }
        //
        // Summary:
        //     The user who created the pull request.
        public GitUser User { get; set; }
        //
        // Summary:
        //     Total number of comments contained in the pull request.
        public int Comments { get; set; }
        //
        // Summary:
        //     Total number of commits contained in the pull request.
        public int Commits { get; set; }
        //
        // Summary:
        //     Total number of additions contained in the pull request.
        public int Additions { get; set; }
        //
        // Summary:
        //     Total number of deletions contained in the pull request.
        public int Deletions { get; set; }
        //
        // Summary:
        //     Total number of files changed in the pull request.
        public int ChangedFiles { get; set; }
        //
        // Summary:
        //     The user who merged the pull request.
        public GitUser MergedBy { get; set; }
        //
        // Summary:
        //     The BASE reference for the pull request.
        public GithubReference Base { get; set; }
        //
        // Summary:
        //     When the pull request was merged.
        public DateTimeOffset? MergedAt { get; set; }
        //
        // Summary:
        //     If the issue is locked or not
        public bool Locked { get; set; }
        //
        // Summary:
        //     The internal Id for this pull request (not the pull request number)
        public long Id { get; set; }
        //
        // Summary:
        //     The URL for this pull request.
        public string Url { get; set; }
        //
        // Summary:
        //     The URL for the pull request page.
        public string HtmlUrl { get; set; }
        //
        // Summary:
        //     The URL for the pull request's diff (.diff) file.
        public string DiffUrl { get; set; }
        //
        // Summary:
        //     The URL for the pull request's patch (.patch) file.
        public string PatchUrl { get; set; }
        //
        // Summary:
        //     The URL for the specific pull request issue.
        public string IssueUrl { get; set; }
        //
        // Summary:
        //     The HEAD reference for the pull request.
        public GithubReference Head { get; set; }
        //
        // Summary:
        //     The URL for the pull request statuses.
        public string StatusesUrl { get; set; }
        //
        // Summary:
        //     Whether the pull request is open or closed. The default is Octokit.ItemState.Open.
        public GitItemState State { get; set; }
        //
        // Summary:
        //     Title of the pull request.
        public string Title { get; set; }
        //
        // Summary:
        //     The body (content) contained within the pull request.
        public string Body { get; set; }
        //
        // Summary:
        //     When the pull request was created.
        public DateTimeOffset CreatedAt { get; set; }
        //
        // Summary:
        //     When the pull request was last updated.
        public DateTimeOffset UpdatedAt { get; set; }
        //
        // Summary:
        //     When the pull request was closed.
        public DateTimeOffset? ClosedAt { get; set; }
        //
        // Summary:
        //     The pull request number.
        public int Number { get; set; }
        //
        // Summary:
        //     Users requested for review
        public IReadOnlyList<GitUser> RequestedReviewers { get; set; }
    }
}
