using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GitIssue
    {
        //
        // Summary:
        //     If the issue is locked or not.
        public bool Locked { get; set; }
        //
        // Summary:
        //     The date the issue was last updated.
        public DateTimeOffset? UpdatedAt { get; set; }
        //
        // Summary:
        //     The date the issue was created.
        public DateTimeOffset CreatedAt { get; set; }
        //
        // Summary:
        //     The date the issue was closed if closed.
        public DateTimeOffset? ClosedAt { get; set; }
        public GitPullRequest PullRequest { get; set; }
        //
        // Summary:
        //     The number of comments on the issue.
        public int Comments { get; set; }
        //
        // Summary:
        //     The milestone, if any, that this issue is assigned to.
        public GitMilestone Milestone { get; set; }
        //
        // Summary:
        //     The multiple users this issue is assigned to.
        public IReadOnlyList<GitUser> Assignees { get; set; }
        //
        // Summary:
        //     The user this issue is assigned to.
        public GitUser Assignee { get; set; }
        //
        // Summary:
        //     The set of labels applied to the issue
        public IReadOnlyList<GitLabel> Labels { get; set; }
        //
        // Summary:
        //     The user that created the issue.
        public GitUser User { get; set; }
        //
        // Summary:
        //     Details about the user who has closed this issue.
        public GitUser ClosedBy { get; set; }
        //
        // Summary:
        //     Details about the issue.
        public string Body { get; set; }
        //
        // Summary:
        //     Title of the issue
        public string Title { get; set; }
        //
        // Summary:
        //     Whether the issue is open or closed.
        public GitItemState State { get; set; }
        //
        // Summary:
        //     The issue number.
        public int Number { get; set; }
        //
        // Summary:
        //     The Events URL of this issue.
        public string EventsUrl { get; set; }
        //
        // Summary:
        //     The Comments URL of this issue.
        public string CommentsUrl { get; set; }
        //
        // Summary:
        //     The URL for the HTML view of this issue.
        public string HtmlUrl { get; set; }
        //
        // Summary:
        //     The URL for this issue.
        public string Url { get; set; }
        //
        // Summary:
        //     The internal Id for this issue (not the issue number)
        public int Id { get; set; }
        //
        // Summary:
        //     The repository the issue comes from.
        public GitRepository Repository { get; set; }
        public GitReactionSummary Reactions { get; set; }
    }
}
