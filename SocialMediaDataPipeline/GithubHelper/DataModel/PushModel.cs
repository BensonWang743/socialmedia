using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubHelper.DataModel
{
    using System;
    using System.Collections.Generic;
    public class Commit
    {
        public string sha { get; set; }
        public bool? distinct { get; set; }
        public string message { get; set; }
        public string url { get; set; }
        public Author author { get; set; }
    }
    public class Author
    {
        public string name { get; set; }
        public string email { get; set; }
    }

    public class PushModel
    {
        public long? push_id { get; set; }
        public string head { get; set; }
        public string @ref { get; set; }
        public string before { get; set; }
        public int? size { get; set; }
        public int? distinct_size { get; set; }
        public List<Commit> commits { get; set; }

    }
}
