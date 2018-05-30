using AzureDataLakeUDO.GithubModel;
using Microsoft.Analytics.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO
{
    [SqlUserDefinedExtractor(AtomicFileProcessing = false)]
    public class GithubIssueExtractor : IExtractor
    {
        private Encoding _encoding;
        private byte[] _rowDelim;

        public GithubIssueExtractor(Encoding encoding, string rowDelim = "\r\n")
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
                        GitIssue issue = JsonConvert.DeserializeObject<GitIssue>(line);
                        output.Set("locked", issue.Locked);
                        output.Set("updatedAt", issue.UpdatedAt==null?(DateTime?)null:issue.UpdatedAt.Value.UtcDateTime);
                        output.Set("createdAt", issue.CreatedAt.UtcDateTime);
                        output.Set("closedAt", issue.ClosedAt==null?(DateTime?)null:issue.ClosedAt.Value.UtcDateTime);
                        output.Set("pullRequest_RepoId", issue.PullRequest==null?(long?)null:issue.PullRequest.Id);
                        output.Set("pullRequest_Number", issue.PullRequest==null?(int?)null:issue.PullRequest.Number);
                        output.Set("comments", issue.Comments);
                        output.Set("milestone", issue.Milestone==null?null:JsonConvert.SerializeObject(issue.Milestone));
                        output.Set("assignees", issue.Assignees==null?null:JsonConvert.SerializeObject(issue.Assignees));
                        output.Set("assignee", issue.Assignee==null?(int?)null:issue.Assignee.Id);
                        output.Set("labels", issue.Labels==null?null:JsonConvert.SerializeObject(issue.Labels));
                        output.Set("user", issue.User==null?(int?)null:issue.User.Id);
                        output.Set("closedBy", issue.ClosedBy==null?(int?)null:issue.ClosedBy.Id);
                        output.Set("body", issue.Body==null?null:issue.Body.Length>2050?issue.Body.Substring(0,2048):issue.Body);
                        output.Set("title", issue.Title);
                        output.Set("state", issue.State.StringValue);
                        output.Set("number", issue.Number);
                        output.Set("eventUrl", issue.EventsUrl);
                        output.Set("commentsUrl", issue.CommentsUrl);
                        output.Set("htmlUrl", issue.HtmlUrl);
                        output.Set("url", issue.Url);
                        output.Set("id", issue.Id);
                        output.Set("repository_Id", issue.Repository==null?(long?)null:issue.Repository.Id);
                        output.Set("reactions", issue.Reactions==null?null:JsonConvert.SerializeObject(issue.Reactions));
                    }
                }
                yield return output.AsReadOnly();
            }
        }
    }
}
