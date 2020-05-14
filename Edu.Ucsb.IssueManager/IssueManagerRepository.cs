using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edu.Ucsb.Core.Extensions;
using Edu.Ucsb.IssueManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Edu.Ucsb.IssueManager
{
    public class IssueManagerRepository : IIssueManagerRepository
    {
        public IssueManagerRepository(
            ILogger<IssueManagerRepository> logger,
            IssueManagerContext db)
        {
            this._logger = logger;
            this._db = db;
        }

        public async Task DeleteIssueAsync(int issueId)
        {
            using(var transaction = await this._db.Database.BeginTransactionAsync())
            {
                try
                {
                    var issue = await this._db.FindAsync<Issue>(issueId);
                    var userIssue = await this._db.FindAsync<UserIssue>(issueId);

                    this._db.Remove(issue);
                    this._db.Remove(userIssue);

                    this._db.SaveChanges();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    this._logger?.LogError(ex, ex.Message);
                }
            }
        }

        public async Task<Issue> InsertIssueAsync(string userId, Issue data)
        {
            Issue issue = null;

            using(var transaction = await this._db.Database.BeginTransactionAsync())
            {
                try
                {
                    this._db.Add(data);
                    var issueId = this._db.SaveChanges();

                    var userIssue = new UserIssue
                    {
                        IssueId = issueId,
                        UserId = userId
                    };
                    this._db.Add(userIssue);
                    this._db.SaveChanges();

                    await transaction.CommitAsync();

                    issue = await this._db.FindAsync<Issue>(issueId);
                }
                catch (Exception ex)
                {
                    this._logger?.LogError(ex, ex.Message);
                }
            }

            return issue;
        }

        public async Task<IEnumerable<Issue>> ListIssuesAsync(string userId)
        {
            return await this._db.Issues
                .Where(i => i.UserIssue.UserId == userId)
                .ToArrayAsync();
        }

        public async Task<Issue> UpdateIssueAsync(Issue data)
        {
            this._db.Entry(data).State = EntityState.Modified;
            await this._db.SaveChangesAsync();

            return data;
        }

        private readonly ILogger<IssueManagerRepository> _logger;
        private readonly IssueManagerContext _db;
    }
}