using AnagramAPI.Abstraction;
using AnagramAPI.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnagramController : ControllerBase
    {


        private readonly ILogger<AnagramController> _logger;
        private IProcessor _processor;
        private readonly IConfiguration _config;
        public AnagramController(ILogger<AnagramController> logger, IProcessor processor, IConfiguration config)
        {
            _logger = logger;
            _processor = processor;
            _config = config;
        }

        [HttpGet]
        public IActionResult Get(string word)
        {
            if (!String.IsNullOrEmpty(word))
            {
                try
                {
                    Request request = new Request()
                    {
                        FilePath = _config.GetValue<string>("FilePath"),
                        RequestGuid = Guid.NewGuid().ToString(),
                        WordToFind = word.Trim().ToLower()
                    };
                    string result = _processor.FindAnagrams(request);
                    if (!String.IsNullOrEmpty(result))
                    {
                        Console.WriteLine(string.Format("Anagram words found : {0}", result));
                        return Ok(result);
                    }
                    else
                    {
                        Console.WriteLine(string.Format("No anagram words found for {0} . Please retry with different word.", word));
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception occured in Main method : {0}", Utility.Helper.GetExceptionMessage(ex));
                    return StatusCode(500, "Internal Error Occured");
                }
               
                
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
