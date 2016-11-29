using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VstsNotifications.Models.Payloads.PullRequest;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Webhooks.Interfaces;
using VstsNotifications.Webhooks.Properties;

namespace VstsNotifications.Webhooks.Controllers
{
    [Route("api/[controller]")]
    public class WebhooksController : Controller
    {
        private readonly Settings _settings;

        private readonly IMessageMapper _messageMapper;
        private readonly IMessageService _messageService;

        public WebhooksController (IOptions<Settings> settings, IMessageMapper messageMapper, IMessageService messageService)
        {
            _settings = settings.Value;

            _messageMapper = messageMapper;
            _messageService = messageService;          
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
            var message = _messageMapper.MapMessage(payload, _settings);
            var result = _messageService.NotifyReviewers(message);
            
            return CreatedAtAction("Post", result);
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
