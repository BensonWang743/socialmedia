using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubHelper.DataModel
{
    public class Label
    {
        public int? id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public bool? @default { get; set; }
    }

    public class Issue
    {
        public string url { get; set; }
        public string labels_url { get; set; }
        public string repository_url { get; set; }
        public string comments_url { get; set; }
        public string events_url { get; set; }
        public string html_url { get; set; }
        public long? id { get; set; }
        public int? number { get; set; }
        public string title { get; set; }
        public User user { get; set; }
        public List<Label> labels { get; set; }
        public string state { get; set; }
        public bool? locked { get; set; }
        public User assignee { get; set; }
        public List<User> assignees { get; set; }
        public object milestone { get; set; }
        public int? comments { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? closed_at { get; set; }
        public string body { get; set; }
        public string author_association { get; set; }
    }





    public class IssueModel
    {
        public string action { get; set; }
        public Issue issue { get; set; }
        public object changes { get; set; }
        public object assignee { get; set; }
        public object label { get; set; }
    }
}
