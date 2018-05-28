using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GitAccount
    {
        //
        // Summary:
        //     Number of public repos the account owns.
        public int PublicRepos { get; set; }
        //
        // Summary:
        //     Number of public gists the account has created.
        public int PublicGists { get; set; }
        //
        // Summary:
        //     Number of private gists the account has created.
        public int? PrivateGists { get; set; }
        //
        // Summary:
        //     Plan the account pays for.
        public GitPlan Plan { get; set; }
        //
        // Summary:
        //     Number of private repos owned by the account.
        public int OwnedPrivateRepos { get; set; }
        //
        // Summary:
        //     The type of account associated with this entity
        public AccountType? Type { get; set; }
        //
        // Summary:
        //     The account's full name.
        public string Name { get; set; }
        //
        // Summary:
        //     The account's login.
        public string Login { get; set; }
        //
        // Summary:
        //     The account's geographic location.
        public string Location { get; set; }
        //
        // Summary:
        //     The account's system-wide unique Id.
        public int Id { get; set; }
        //
        // Summary:
        //     The HTML URL for the account on github.com (or GitHub Enterprise).
        public string HtmlUrl { get; set; }
        //
        // Summary:
        //     Indicates whether the account is currently hireable.
        public bool? Hireable { get; set; }
        //
        // Summary:
        //     Number of other users the account is following.
        public int Following { get; set; }
        //
        // Summary:
        //     Number of follwers the account has.
        public int Followers { get; set; }
        //
        // Summary:
        //     The account's email.
        public string Email { get; set; }
        //
        // Summary:
        //     Amount of disk space the account is using.
        public int? DiskUsage { get; set; }
        //
        // Summary:
        //     Date the account was created.
        public DateTimeOffset CreatedAt { get; set; }
        //
        // Summary:
        //     Company the account works for.
        public string Company { get; set; }
        //
        // Summary:
        //     Number of collaborators the account has.
        public int? Collaborators { get; set; }
        //
        // Summary:
        //     URL of the account's blog.
        public string Blog { get; set; }
        //
        // Summary:
        //     The account's bio.
        public string Bio { get; set; }
        //
        // Summary:
        //     URL of the account's avatar.
        public string AvatarUrl { get; set; }
        //
        // Summary:
        //     Total number of private repos the account owns.
        public int TotalPrivateRepos { get; set; }
        //
        // Summary:
        //     The account's API URL.
        public string Url { get; set; }
    }
}
