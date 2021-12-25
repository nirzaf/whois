using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Whois;

namespace whoisit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WhoisController : ControllerBase
    {
        private readonly ILogger<WhoisController> _logger;

        public WhoisController(ILogger<WhoisController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{query}")]
        public async Task<IActionResult> GetAsync(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest();
            }

            _logger.LogInformation("Starting whois for {0}", query);
            var lookup = new WhoisLookup();
            var result = await lookup.LookupAsync(query);

            _logger.LogInformation("Whois query for {0} completed with status {1}", query, result.Status);
            return Ok(result.Content);
        }
    }
}
