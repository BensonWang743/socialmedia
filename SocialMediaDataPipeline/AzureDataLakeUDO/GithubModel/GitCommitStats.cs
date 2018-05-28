using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GitCommitStats
    {
        //
        // 摘要:
        //     The number of additions made within the commit
        public int Additions { get;  set; }
        //
        // 摘要:
        //     The number of deletions made within the commit
        public int Deletions { get;  set; }
        //
        // 摘要:
        //     The total number of modifications within the commit
        public int Total { get;  set; }

    }
}
