using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenBased.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        [Route("/Error")]
        public IActionResult Error()
        {
            try
            {
                var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                _logger.Log(LogLevel.Error, "An Exception occurred on - {0}. Message - {1}\n**Stack Trace**\n{2}",
                    exceptionDetails.Path, exceptionDetails.Error.Message, exceptionDetails.Error.StackTrace);
            }
            catch(Exception)
            {

            }
            return View();
        }
    }
}
