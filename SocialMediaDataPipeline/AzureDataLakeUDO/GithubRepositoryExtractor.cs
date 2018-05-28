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
    public class GithubRepositoryExtractor : IExtractor
    {
        private Encoding _encoding;
        private byte[] _rowDelim;

        public GithubRepositoryExtractor(Encoding encoding, string rowDelim ="\r\n")
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
                        GitRepository repo = JsonConvert.DeserializeObject<GitRepository>(line);
                        output.Set("openIssuesCount", repo.OpenIssuesCount);
                        output.Set("pushedAt", repo.PushedAt == null ? (DateTime?)null : repo.PushedAt.Value.UtcDateTime);
                        output.Set("createdAt", repo.CreatedAt.UtcDateTime);
                        output.Set("updatedAt", repo.UpdatedAt.UtcDateTime);
                        output.Set("permissions_admin", repo.Permissions == null ? (bool?)null : repo.Permissions.Admin);
                        output.Set("permissions_push", repo.Permissions == null ? (bool?)null : repo.Permissions.Push);
                        output.Set("permissions_pull", repo.Permissions == null ? (bool?)null : repo.Permissions.Pull);
                        output.Set("parentRepoId", repo.Parent == null ? (long?)null : repo.Parent.Id);
                        output.Set("sourceRepoId", repo.Source == null ? (long?)null : repo.Source.Id);
                        output.Set("hasIssues", repo.HasIssues);
                        output.Set("defaultBranch", repo.DefaultBranch);
                        output.Set("hasWiki", repo.HasWiki);
                        output.Set("hasDownload", repo.HasDownloads);
                        output.Set("allowRebaseMerge", repo.AllowRebaseMerge);
                        output.Set("allowSquashMerge", repo.AllowSquashMerge);
                        output.Set("allowMergeCommit", repo.AllowMergeCommit);
                        output.Set("hasPages", repo.HasPages);
                        output.Set("licenseMetadata_Key", repo.License == null ? null : repo.License.Key);
                        output.Set("licenseMetadata_Name", repo.License == null ? null : repo.License.Name);
                        output.Set("licenseMetadata_SpdxId", repo.License == null ? null : repo.License.SpdxId);
                        output.Set("licenseMetadata_Url", repo.License == null ? null : repo.License.Url);
                        output.Set("licenseMetadata_Featured", repo.License == null ? (bool?)null : repo.License.Featured);
                        output.Set("stargazersCount", repo.StargazersCount);
                        output.Set("forksCount", repo.ForksCount);
                        output.Set("fork", repo.Fork);
                        output.Set("url", repo.Url);
                        output.Set("htmlUrl", repo.HtmlUrl);
                        output.Set("cloneUrl", repo.CloneUrl);
                        output.Set("gitUrl", repo.GitUrl);
                        output.Set("sshUrl", repo.SshUrl);
                        output.Set("svnUrl", repo.SvnUrl);
                        output.Set("mirrorUrl", repo.MirrorUrl);
                        output.Set("id", repo.Id);
                        output.Set("owner", repo.Owner==null?(long?)null:repo.Owner.Id);
                        output.Set("name", repo.Name);
                        output.Set("fullName", repo.FullName);
                        output.Set("description", repo.Description);
                        output.Set("homepage", repo.Homepage);
                        output.Set("language", repo.Language);
                        output.Set("private", repo.Private);
                        output.Set("subscribersCount", repo.SubscribersCount);
                        output.Set("size", repo.Size);
                    }
                }
                yield return output.AsReadOnly();
            }
        }
    }
}
