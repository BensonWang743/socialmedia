using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GitMilestone
    {
        //
        // Summary:
        //     The URL for this milestone.
        public string Url { get; set; }
        //
        // Summary:
        //     The Html page for this milestone.
        public string HtmlUrl { get; set; }
        //
        // Summary:
        //     The milestone number.
        public int Number { get; set; }
        //
        // Summary:
        //     Whether the milestone is open or closed.
        public GitItemState State { get; set; }
        //
        // Summary:
        //     Title of the milestone.
        public string Title { get; set; }
        //
        // Summary:
        //     Optional description for the milestone.
        public string Description { get; set; }
        //
        // Summary:
        //     The user that created this milestone.
        public GitUser Creator { get; set; }
        //
        // Summary:
        //     The number of open issues in this milestone.
        public int OpenIssues { get; set; }
        //
        // Summary:
        //     The number of closed issues in this milestone.
        public int ClosedIssues { get; set; }
        //
        // Summary:
        //     The date this milestone was created.
        public DateTimeOffset CreatedAt { get; set; }
        //
        // Summary:
        //     The date, if any, when this milestone is due.
        public DateTimeOffset? DueOn { get; set; }
        //
        // Summary:
        //     The date, if any, when this milestone was closed.
        public DateTimeOffset? ClosedAt { get; set; }
        //
        // Summary:
        //     The date, if any, when this milestone was updated.
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
