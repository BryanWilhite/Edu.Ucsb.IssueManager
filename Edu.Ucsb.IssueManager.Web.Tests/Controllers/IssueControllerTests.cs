using System;
using System.IO;
using System.Threading.Tasks;
using Edu.Ucsb.Core;
using Edu.Ucsb.Core.Extensions;
using Edu.Ucsb.IssueManager.Models;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Edu.Ucsb.IssueManager.Web.Tests.Controllers
{
    public class IssueControllerTests
    {
        public IssueControllerTests(ITestOutputHelper helper)
        {
            this._testOutputHelper = helper;

            var projectRoot = FrameworkAssemblyUtility.GetPathFromAssembly(this.GetType().Assembly, "../../../");
            var projectInfo = new DirectoryInfo(projectRoot);
            Assert.True(projectInfo.Exists);

            var basePath = projectInfo.Parent.FindDirectory("Edu.Ucsb.IssueManager.Web").FullName;

            var builder = Program.CreateWebHostBuilder(args: null, builderAction: (builderContext, configBuilder) =>
            {
                Assert.NotNull(builderContext);

                this._testOutputHelper.WriteLine($"configuring {nameof(TestServer)} with {nameof(basePath)}: {basePath}...");


                var env = builderContext.HostingEnvironment;
                Assert.NotNull(env);

                env.ContentRootPath = basePath;
                env.EnvironmentName = AppScalars.EnvironmentNameForAutomatedTesting;
                env.WebRootPath = $"{basePath}{Path.DirectorySeparatorChar}wwwroot";

                configBuilder.SetBasePath(env.ContentRootPath);
            });

            Assert.NotNull(builder);

            this._server = new TestServer(builder);
        }

        [Theory]
        [InlineData("index")]
        public async Task ShouldListIssuesAsync(string path)
        {
            var uri = new Uri(string.Concat(baseRoute, path), UriKind.Relative);
            var client = this._server.CreateClient();
            var response = await client.GetAsync(uri);

            response.EnsureSuccessStatusCode();
        }

        const string baseRoute = "issue/api/v1/";

        readonly ITestOutputHelper _testOutputHelper;
        readonly TestServer _server;
    }
}