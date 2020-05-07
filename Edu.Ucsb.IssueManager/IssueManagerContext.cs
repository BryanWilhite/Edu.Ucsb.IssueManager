using Edu.Ucsb.IssueManager.Extensions;
using Edu.Ucsb.IssueManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Edu.Ucsb.IssueManager
{
    public class IssueManagerContext : DbContext
    {
        public DbSet<Issue> Issues { get; set; }

        public DbSet<UserIssue> UserIssues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Server=localhost;Database=Edu.Ucla.IssueManager.Web;MultipleActiveResultSets=true;User id=SA;Password=@one#2Three;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .MapIssue()
                .MapUserIssue();
        }

        internal IConfiguration Configuration { get; }
    }
}