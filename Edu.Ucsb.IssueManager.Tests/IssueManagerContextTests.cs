using System.IO;
using Edu.Ucsb.Core;
using Edu.Ucsb.Core.Extensions;
using Edu.Ucsb.IssueManager.Models;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Edu.Ucsb.IssueManager.Tests
{
    public class IssueManagerContextTests
    {
        public IssueManagerContextTests(ITestOutputHelper helper)
        {
            this._testOutputHelper = helper;

            var projectRoot = FrameworkAssemblyUtility
                .GetPathFromAssembly(this.GetType().Assembly, "../../../");
            var projectInfo = new DirectoryInfo(projectRoot);

            var basePath = projectInfo.Parent.FindDirectory("Edu.Ucsb.IssueManager.Web").FullName;

            this._configuration = ProgramUtility.LoadConfiguration(basePath);
        }

        [Fact]
        public void ShouldDoCrudOperation()
        {
            this._testOutputHelper.WriteLine($"using {nameof(IssueManagerContext)}...");

            using(var db = new IssueManagerContext(this._configuration))
            {
                var issue = new Issue
                    {
                        Title = "test one issue",
                        Description = "this is a test",
                        Status = IssueStatus.Open
                    };
                db.Add(issue);

                this._testOutputHelper.WriteLine("create...");
                var issueId = db.SaveChanges();
                Assert.True(issueId > 0);
                this._testOutputHelper.WriteLine($"issue `{issueId}` created");

                this._testOutputHelper.WriteLine("read...");
                issue = db.Find<Issue>(issueId);
                Assert.NotNull(issue);
                this._testOutputHelper.WriteLine($"read: {issue}");

                var newDescription = "another test";
                issue.Description = newDescription;
                this._testOutputHelper.WriteLine("update...");
                db.SaveChanges();

                issue = db.Find<Issue>(issueId);
                Assert.Equal(newDescription, issue.Description);
                this._testOutputHelper.WriteLine($"updated: {issue}");

                this._testOutputHelper.WriteLine("delete...");
                db.Remove(issue);
                db.SaveChanges();

                issue = db.Find<Issue>(issueId);
                Assert.Null(issue);
                this._testOutputHelper.WriteLine("deleted");
            }

        }

        readonly ITestOutputHelper _testOutputHelper;

        readonly IConfigurationRoot _configuration;
    }
}
