using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VstsNotifications.Entities;
using VstsNotifications.Webhooks.Models.PullRequest;
using VstsNotifications.Webhooks.Properties;

namespace VstsNotifications.Webhooks.Controllers
{
    [Route("api/[controller]")]
    public class WebhooksController : Controller
    {
        private readonly Settings _settings;

        public WebhooksController (IOptions<Settings> settings)
        {
            _settings = settings.Value;          
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]PullRequestPayload payload)
        {
            var returnObject = new { date = payload, contributors = _settings };
            return CreatedAtAction("Post", returnObject);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
