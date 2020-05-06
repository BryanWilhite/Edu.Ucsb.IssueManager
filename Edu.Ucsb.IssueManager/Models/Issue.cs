namespace Edu.Ucsb.IssueManager.Models
{
    public class Issue
    {
        public int IssueId { get; set; }

        public string Description { get; set; }

        public IssueStatus Status { get; set; }

        public string Title { get; set; }

        public UserIssue UserIssue { get; set; }
    }
}