using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Edu.Ucsb.Core;
using Edu.Ucsb.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Edu.Ucsb.IssueManager.Tests
{
    public class IssueManagerRepositoryTest
    {
        public IssueManagerRepositoryTest(ITestOutputHelper helper)
        {
            this._testOutputHelper = helper;

            var projectRoot = FrameworkAssemblyUtility
                .GetPathFromAssembly(this.GetType().Assembly, "../../../");
            var projectInfo = new DirectoryInfo(projectRoot);

            var basePath = projectInfo.Parent.FindDirectory("Edu.Ucsb.IssueManager.Web").FullName;

            this._configuration = ProgramUtility.LoadConfiguration(basePath);
        }

        [Theory]
        [InlineData("not-a-user", false)]
        public async Task ShouldListIssuesAsync(string userId, bool shouldReturnData)
        {
            this._testOutputHelper.WriteLine($"using {nameof(IssueManagerContext)}...");

            using(var db = new IssueManagerContext(this._configuration))
            {
                var repository = new IssueManagerRepository(null, db);
                var issues = await repository.ListIssuesAsync(userId);
                Assert.Equal(shouldReturnData, issues.Any());
            }
        }

        readonly ITestOutputHelper _testOutputHelper;

        readonly IConfigurationRoot _configuration;
    }
}