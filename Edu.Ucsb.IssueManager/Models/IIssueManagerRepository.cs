using System.Collections.Generic;
using System.Threading.Tasks;

namespace Edu.Ucsb.IssueManager.Models
{
    public interface IIssueManagerRepository
    {
        Task DeleteIssueAsync(int issueId);

        Task<Issue> InsertIssueAsync(string userId, Issue data);

        Task<IEnumerable<Issue>> ListIssuesAsync(string userId);

        Task<Issue> UpdateIssueAsync(Issue data);
    }
}