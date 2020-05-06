using Edu.Ucsb.IssueManager.Models;
using Microsoft.EntityFrameworkCore;

namespace Edu.Ucsb.IssueManager
{
    public class IssueManagerContext : DbContext
    {
        public IssueManagerContext(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public DbSet<Issue> Issues { get; set; }

        public DbSet<UserIssue> UserIssues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(this._connectionString);

        private readonly string _connectionString;
    }
}