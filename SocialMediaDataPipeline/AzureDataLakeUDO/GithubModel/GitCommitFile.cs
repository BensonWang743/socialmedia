using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeUDO.GithubModel
{
    public class GitCommitFile
    {
        //
        // 摘要:
        //     The name of the file
        public string Filename { get;  set; }
        //
        // 摘要:
        //     Number of additions performed on the file.
        public int Additions { get;  set; }
        //
        // 摘要:
        //     Number of deletions performed on the file.
        public int Deletions { get;  set; }
        //
        // 摘要:
        //     Number of changes performed on the file.
        public int Changes { get;  set; }
        //
        // 摘要:
        //     File status, like modified, added, deleted.
        public string Status { get;  set; }
        //
        // 摘要:
        //     The url to the file blob.
        public string BlobUrl { get;  set; }
        //
        // 摘要:
        //     The url to file contents API.
        public string ContentsUrl { get;  set; }
        //
        // 摘要:
        //     The raw url to download the file.
        public string RawUrl { get;  set; }
        //
        // 摘要:
        //     The SHA of the file.
        public string Sha { get;  set; }
        //
        // 摘要:
        //     The patch associated with the commit
        public string Patch { get;  set; }
        //
        // 摘要:
        //     The previous filename for a renamed file.
        public string PreviousFileName { get;  set; }
    }
}
