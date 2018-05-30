using AzureDataLakeUDO.GithubModel;
using Microsoft.Analytics.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * todo:
 max size for each row is 4 MB
 need to use byte[] instead
     */
namespace AzureDataLakeUDO
{
    [SqlUserDefinedExtractor(AtomicFileProcessing = false)]
    public class GithubCommitExtractor : IExtractor
    {
        private Encoding _encoding;
        private byte[] _rowDelim;

        public GithubCommitExtractor(Encoding encoding, string rowDelim = "\r\n")
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
                        GithubCommits commit = JsonConvert.DeserializeObject<GithubCommits>(line);
                        output.Set("authorId", commit.Author == null ? (int?)null : commit.Author.Id);
                        output.Set("commentsUrl", commit.CommentsUrl);
                        output.Set("commit_AuthorMessage", commit.Commit == null ? null : commit.Commit.Message);
                        output.Set("commit_AuthorEmail", commit.Commit == null ? null : commit.Commit.Author == null ? null : commit.Commit.Author.Email);
                        output.Set("commit_AuthorDate", commit.Commit == null ? (DateTime?)null : commit.Commit.Author == null ? (DateTime?)null : commit.Commit.Author.Date.UtcDateTime);
                        output.Set("commit_CommitterEmail", commit.Commit == null ? null : commit.Commit.Committer == null ? null : commit.Commit.Committer.Email);
                        output.Set("commit_CommitterDate", commit.Commit == null ? (DateTime?)null : commit.Commit.Committer == null ? (DateTime?)null : commit.Commit.Committer.Date.UtcDateTime);
                        output.Set("commit_Tree", commit.Commit == null ? null : commit.Commit.Tree == null ? null : JsonConvert.SerializeObject(commit.Commit.Tree));
                        output.Set("commit_Parents", commit.Commit == null ? null : commit.Commit.Parents == null ? null : JsonConvert.SerializeObject(commit.Commit.Parents));
                        output.Set("commit_CommentCount", commit.Commit == null ? (int?)null : commit.Commit.CommentCount);
                        output.Set("commit_Verification", commit.Commit == null ? null : commit.Commit.Verification == null ? null : JsonConvert.SerializeObject(commit.Commit.Verification));
                        output.Set("committerId", commit.Committer == null ? (int?)null : commit.Committer.Id);
                        output.Set("htmlUrl", commit.HtmlUrl);
                        output.Set("stats_Additions", commit.Stats == null ? (int?)null : commit.Stats.Additions);
                        output.Set("stats_Deletions", commit.Stats == null ? (int?)null : commit.Stats.Deletions);
                        output.Set("stats_Total", commit.Stats == null ? (int?)null : commit.Stats.Total);
                        output.Set("parents", commit.Parents == null ? null : JsonConvert.SerializeObject(commit.Parents));
                        //output.Set("files", commit.Files == null ? null : Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(commit.Files)));
                        output.Set("url",commit.Url);
                        output.Set("label", commit.Label);
                        output.Set("ref", commit.Ref);
                        output.Set("sha", commit.Sha);
                        output.Set("userId", commit.User==null?(int?)null:commit.User.Id);
                        output.Set("repositoryId", commit.Repository==null?(long?)null:commit.Repository.Id);
                        List<GitProcessedFiles> processedFiles = new List<GitProcessedFiles>();
                        if (commit.Files != null)
                        {
                            foreach (var f in commit.Files)
                            {
                                processedFiles.Add(new GitProcessedFiles()
                                {
                                    Status = f.Status,
                                    RawUrl = f.RawUrl,
                                    PreviousFileName = f.PreviousFileName
                                });
                            }
                            output.Set("files", JsonConvert.SerializeObject(processedFiles));
                        }
                        else
                            output.Set("files",(string)null);

                    }
                }
                yield return output.AsReadOnly();
            }
        }
    }
}
