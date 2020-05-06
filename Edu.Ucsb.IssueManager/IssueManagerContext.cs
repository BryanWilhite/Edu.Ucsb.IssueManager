using Edu.Ucsb.IssueManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Edu.Ucsb.IssueManager
{
    public class IssueManagerContext : DbContext
    {
        public DbSet<Issue> Issues { get; set; }

        public DbSet<UserIssue> UserIssues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

        internal IConfiguration Configuration { get; }
    }
}