using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApp.Dtos;

namespace TestApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private static readonly ConcurrentBag<SampleDto> _data = new ConcurrentBag<SampleDto>();
        private readonly ILogger<SampleController> _logger;

        public SampleController(ILogger<SampleController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(SampleDto input)
        {
            _data.Add(input);
            return Ok();
        }

        [HttpGet]
        public IEnumerable<SampleDto> Get(int start = -1)
        {
            IEnumerable<SampleDto> retVal = _data;
            if (start > 0)
            {
                retVal = _data.Skip(start);
            }
            return retVal.Take(5).ToArray();
        }
    }
}
