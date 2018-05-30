using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GitVerification
    {
        public bool Verified { get;  set; }
        //
        // 摘要:
        //     The reason for verified value.
        public GitItemState Reason { get;  set; }
        //
        // 摘要:
        //     The signature that was extracted from the commit.
        public string Signature { get;  set; }
        //
        // 摘要:
        //     The value that was signed.
        public string Payload { get;  set; }
    }
}
