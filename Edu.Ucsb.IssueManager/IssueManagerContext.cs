using Edu.Ucsb.IssueManager.Extensions;
using Edu.Ucsb.IssueManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Edu.Ucsb.IssueManager
{
    public class IssueManagerContext : DbContext
    {
        public IssueManagerContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public DbSet<Issue> Issues { get; set; }

        public DbSet<UserIssue> UserIssues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(this._configuration.GetConnectionString("DefaultConnection"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .MapIssue()
                .MapUserIssue();
        }

        private readonly IConfiguration _configuration;
    }
}