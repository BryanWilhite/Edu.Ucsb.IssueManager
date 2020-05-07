using System;
using System.Threading.Tasks;
using Edu.Ucsb.IssueManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Edu.Ucsb.IssueManager.Web.Controllers
{
    [Authorize]
    [Route("[controller]/api/v1")]
    public class IssueController : Controller
    {
        public IssueController(
            Logger<IssueController> logger,
            IIssueManagerRepository repository)
        {
            this._logger = logger;
            this._repository = repository;
            this._userId = this.User?.Identity?.Name;
        }

        [HttpDelete]
        [Route("data/{issueId}")]
        public async Task<IActionResult> InsertAsync(int issueId)
        {
            try
            {
                await this._repository.DeleteIssueAsync(issueId);
                return this.NoContent();
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex, ex.Message);
                return this.Problem();
            }
        }

        [HttpGet]
        [Route("index")]
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var data = await this._repository.ListIssuesAsync(this._userId);
                return this.Ok(data);
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex, ex.Message);
                return this.Problem();
            }
        }

        [HttpPost]
        [Route("data")]
        public async Task<IActionResult> InsertAsync([FromBody] Issue data)
        {
            try
            {
                var issue = await this._repository.InsertIssueAsync(this._userId, data);
                return this.Ok(issue);
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex, ex.Message);
                return this.Problem();
            }
        }

        [HttpPut]
        [Route("data")]
        public async Task<IActionResult> UpdateAsync([FromBody] Issue data)
        {
            try
            {
                var issue = await this._repository.UpdateIssueAsync(data);
                return this.Ok(issue);
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex, ex.Message);
                return this.Problem();
            }
        }

        private readonly ILogger<IssueController> _logger;
        private readonly IIssueManagerRepository _repository;
        private readonly string _userId;
    }
}