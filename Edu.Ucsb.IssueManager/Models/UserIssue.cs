namespace Edu.Ucsb.IssueManager.Models
{
    public class UserIssue
    {
        public Issue Issue { get; set; }

        public int IssueId { get; set; }

        public string UserId { get; set; }
    }
}