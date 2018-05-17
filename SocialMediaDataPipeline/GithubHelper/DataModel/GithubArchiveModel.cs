using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubHelper.DataModel
{
    public class GithubArchiveModel
    {
        public string id { get; set; }
        public string type { get; set; }
        public Actor actor { get; set; }
        public Repository repo { get; set; }
        public object payload { get; set; }
        public bool? @public { get; set; }
        public DateTime? created_at { get; set; }
        public Org org { get; set; }
        public object other { get; set; }

    }
    public class Repository
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Actor
    {
        public int id { get; set; }
        public string login { get; set; }
        public string display_login { get; set; }
        public string gravatar_id { get; set; }
        public string avatar_url { get; set; }
        public string url { get; set; }
    }

    public class Org
    {
        public int id { get; set; }
        public string login { get; set; }
        public string gravatar_id { get; set; }
        public string avatar_url { get; set; }
        public string url { get; set; }

    }
}
