using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Interfaces.Streaming;
using Microsoft.Analytics.Types.Sql;
using Newtonsoft.Json;
using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AzureDataLakeUDO
{
    [SqlUserDefinedExtractor(AtomicFileProcessing =false)]
    public class GithubUserExtractor : IExtractor
    {
        private Encoding _encoding;
        private byte[] _rowDelim;

        public GithubUserExtractor(Encoding encoding,string rowDelim="\r\n")
        {
            this._encoding = ((encoding==null)?Encoding.UTF8:encoding);
            this._rowDelim = this._encoding.GetBytes(rowDelim);
        }
        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            string line = string.Empty;
            foreach (Stream current in input.Split(_rowDelim))
            {
                using (StreamReader streamReader= new StreamReader(current,_encoding))
                {
                    line = streamReader.ReadToEnd().Trim();
                    if (!string.IsNullOrEmpty(line))
                    {
                        //string , string , string , int , string , DateTimeOffset , DateTimeOffset , int , string , int , int , bool? , string , int , int , string , string , string , int , Plan , int , int , 
                        //int , string , RepositoryPermissions , bool , string , DateTimeOffset? 
                        /*
                         long , string , long , long , string 
                         bool admin, bool push, bool 
                         */

                        User user = JsonConvert.DeserializeObject<User>(line);
                        output.Set("avatarUrl", user.AvatarUrl);
                        output.Set("bio",user.Bio);
                        output.Set("blog", user.Blog);
                        output.Set("collaborators", user.Collaborators);
                        output.Set("company", user.Company);
                        output.Set("createdAt", user.CreatedAt.UtcDateTime);
                        output.Set("updatedAt", user.UpdatedAt.UtcDateTime);
                        output.Set("diskUsage", user.DiskUsage);
                        output.Set("followers", user.Followers);
                        output.Set("following", user.Following);
                        output.Set("hireable", user.Hireable);
                        output.Set("htmlUrl", user.HtmlUrl);
                        output.Set("totalPrivateRepos", user.TotalPrivateRepos);
                        output.Set("id", user.Id);
                        output.Set("location", user.Location);
                        output.Set("login", user.Login);
                        output.Set("name", user.Name);
                        output.Set("ownedPrivateRepos", user.OwnedPrivateRepos);
                        output.Set("plan_collaborators",user.Plan==null?(long?)null:user.Plan.Collaborators);
                        output.Set("plan_name", user.Plan == null ? null : user.Plan.Name);
                        output.Set("plan_privateRepos", user.Plan == null ? (long?)null : user.Plan.PrivateRepos);
                        output.Set("plan_space", user.Plan == null ? (long?)null : user.Plan.Space);
                        output.Set("plan_billingEmail", user.Plan == null ? null : user.Plan.BillingEmail);
                        output.Set("privateGists", user.PrivateGists);
                        output.Set("publicGists", user.PublicGists);
                        output.Set("publicRepos", user.PublicRepos);
                        output.Set("url", user.Url);
                        output.Set("permissions_admin", user.Permissions==null?(bool?)null:user.Permissions.Admin);
                        output.Set("permissions_push", user.Permissions == null ? (bool?)null : user.Permissions.Push);
                        output.Set("permissions_pull", user.Permissions == null ? (bool?)null : user.Permissions.Pull);
                        output.Set("siteAdmin", user.SiteAdmin);
                        output.Set("ldapDistinguishedName", user.LdapDistinguishedName);
                        output.Set("suspendedAt", user.SuspendedAt==null?(DateTime?)null:user.SuspendedAt.Value.UtcDateTime);
                        output.Set("Type", user.Type==null?null:user.Type.Value.ToString());
                        output.Set("Suspended", user.Suspended);
                        output.Set("raw",JsonConvert.SerializeObject(user));
                        output.Set("rawstring",line);
                    }

                }
                yield return output.AsReadOnly();
            }
        }
    }
}