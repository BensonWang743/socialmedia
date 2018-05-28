using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GitCommitter
    {
        //
        // 摘要:
        //     Gets the name of the author or committer.
        public string Name { get;  set; }
        //
        // 摘要:
        //     Gets the email of the author or committer.
        public string Email { get;  set; }
        //
        // 摘要:
        //     Gets the date of the author or contributor's contributions.
        public DateTimeOffset Date { get;  set; }
    }
}
