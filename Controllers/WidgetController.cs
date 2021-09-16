using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            var commits = new List<Task>();
            foreach(var i in widgets)
            {
                var widget = await _service.DownloadWidget(i);   //we call out to some services to get this thing...
                widget.Data = widget.Data.ToUpper();             // maybe some kind of processing etc. happens here;
                commits.Add(_repo.CommitWidget(widget));                // then we store it somewhere
            }
            await Task.WhenAll(commits);
            sw.Stop();
            return Ok(new {processed = widgets.Count(), elapsedTime=sw.ElapsedMilliseconds});
        }
    }
}