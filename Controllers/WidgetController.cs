using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Domain;
using Challenge.Repositories;
using Challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WidgetController : ControllerBase
    {
        private readonly IWidgetRepo _repo;
        private readonly IWidgetFetchService _service;

        public WidgetController(IWidgetRepo repo, IWidgetFetchService service)
        {
            _repo = repo;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ProcessWidgets(int amount)
        {
            var widgets = Enumerable.Range(1,amount);
            var sw = new Stopwatch();
            sw.Start();
            var widgetDownloadTasks = widgets.Select(w => _service.DownloadWidget(w));
            var downloadedWidgets = await Task.WhenAll(widgetDownloadTasks);
            var gate = new SemaphoreSlim(15,15); // we know 15 works
            var commits = downloadedWidgets.Select(c => ExecuteCommit(gate, c));
            await Task.WhenAll(commits);
            sw.Stop();
            return Ok(new {processed = widgets.Count(), elapsedTime=sw.ElapsedMilliseconds});
        }

        private async Task ExecuteCommit(SemaphoreSlim gate,  Widget widget)
        {
            await gate.WaitAsync();
            await _repo.CommitWidget(widget);
            gate.Release();
        }
    }
}