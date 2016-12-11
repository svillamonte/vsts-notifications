using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VstsNotifications.Models.Payloads.PullRequest;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Webhooks.Interfaces;
using VstsNotifications.Webhooks.Properties;

namespace VstsNotifications.Webhooks.Controllers
{
    [Route("api/[controller]")]
    public class PullRequestCreatedController : Controller
    {
        private readonly Settings _settings;

        private readonly IMessageMapper _messageMapper;
        private readonly IMessageService _messageService;

        public PullRequestCreatedController (IOptions<Settings> settings, IMessageMapper messageMapper, IMessageService messageService)
        {
            _settings = settings.Value;

            _messageMapper = messageMapper;
            _messageService = messageService;          
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Ready!");
        }

        [HttpPost]
        public IActionResult Post([FromBody]PullRequestPayload payload)
        {
            try 
            {
                var message = _messageMapper.MapMessage(payload, _settings);
                var result = _messageService.NotifyReviewers(message);
                
                if (result) {
                    return Ok("Message posted!");
                }

                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch
            {
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }            
        }
    }
}
