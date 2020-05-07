using Edu.Ucsb.Core.Extensions;
using System;
using System.Text;

namespace Edu.Ucsb.IssueManager.Models
{
    public class Issue
    {
        public int IssueId { get; set; }

        public string Description { get; set; }

        public IssueStatus Status { get; set; }

        public string Title { get; set; }

        public UserIssue UserIssue { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"{nameof(this.Status)}: {this.Status}, ");

            if (!string.IsNullOrWhiteSpace(this.Title))
                sb.Append($"{nameof(this.Title)}: {this.Title}, ");

            if (!string.IsNullOrWhiteSpace(this.Description))
                sb.Append($"{nameof(this.Description)}: {this.Description.Truncate()}");

            return (sb.Length > 0) ?
                sb.ToString() :
                base.ToString();
        }
    }
}