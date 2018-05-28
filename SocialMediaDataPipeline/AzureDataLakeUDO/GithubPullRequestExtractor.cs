using Microsoft.Analytics.Interfaces;
using AzureDataLakeUDO.GithubModel;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace AzureDataLakeUDO
{
    [SqlUserDefinedExtractor(AtomicFileProcessing = false)]
    public class GithubPullRequestExtractor : IExtractor
    {
        private Encoding _encoding;
        private byte[] _rowDelim;

        public GithubPullRequestExtractor(Encoding encoding, string rowDelim = "\r\n")
        {
            this._encoding = ((encoding == null) ? Encoding.UTF8 : encoding);
            this._rowDelim = this._encoding.GetBytes(rowDelim);
        }
        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            string line = string.Empty;
            foreach (Stream current in input.Split(_rowDelim))
            {
                using (StreamReader streamReader = new StreamReader(current, _encoding))
                {
                    line = streamReader.ReadToEnd().Trim();
                    if (!string.IsNullOrEmpty(line))
                    {
                        GitPullRequest pr = JsonConvert.DeserializeObject<GitPullRequest>(line);
                        output.Set("assignee", pr.Assignee==null?(int?)null:pr.Assignee.Id);
                        output.Set("assignees",pr.Assignees==null?null:JsonConvert.SerializeObject(pr.Assignees));
                        output.Set("milestone", pr.Milestone == null ? null : JsonConvert.SerializeObject(pr.Milestone));
                        output.Set("merged", pr.Merged);
                        output.Set("mergeable", pr.Mergeable);
                        output.Set("mergeableState", pr.MergeableState);
                        output.Set("mergeCommitSHA", pr.MergeCommitSha);
                        output.Set("userId", pr.User==null?(int?)null : pr.User.Id);
                        output.Set("comments", pr.Comments);
                        output.Set("commits", pr.Commits);
                        output.Set("additions", pr.Additions);
                        output.Set("deletions",pr.Deletions);
                        output.Set("changedFiles", pr.ChangedFiles);
                        output.Set("mergedBy", pr.MergedBy==null?(int?)null:pr.MergedBy.Id);
                        output.Set("base", pr.Base==null?null:JsonConvert.SerializeObject(pr.Base));
                        output.Set("mergedAt", pr.MergedAt==null?(DateTime?)null:pr.MergedAt.Value.UtcDateTime);
                        output.Set("locked", pr.Locked);
                        output.Set("id", pr.Id);
                        output.Set("url", pr.Url);
                        output.Set("htmlUrl", pr.HtmlUrl);
                        output.Set("diffUrl", pr.DiffUrl);
                        output.Set("patchUrl", pr.PatchUrl);
                        output.Set("issueUrl", pr.IssueUrl);
                        output.Set("head", pr.Head == null ? null : JsonConvert.SerializeObject(pr.Head));
                        output.Set("statusesUrl", pr.StatusesUrl);
                        output.Set("state", pr.State);
                        output.Set("title", pr.Title);
                        output.Set("body", pr.Body);
                        output.Set("createdAt", pr.CreatedAt.UtcDateTime);
                        output.Set("updatedAt", pr.UpdatedAt.UtcDateTime);
                        output.Set("closedAt", pr.ClosedAt==null?(DateTime?)null:pr.ClosedAt.Value.UtcDateTime);
                        output.Set("number",pr.Number);
                        output.Set("requestedReviewers", pr.RequestedReviewers == null ? null : JsonConvert.SerializeObject(pr.RequestedReviewers));
                    }
                }
                yield return output.AsReadOnly();
            }
        }
    }
}