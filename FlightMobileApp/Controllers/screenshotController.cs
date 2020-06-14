using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightMobileApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class screenshotController : ControllerBase
    {
        // GET: /screenshot
        [HttpGet]
        public IActionResult Get()
        {
            // "/sim/paths/screenshot-dir" 
            Byte[] b = System.IO.File.ReadAllBytes(@"E:\\Test.jpg");        
            return File(b, "image/jpeg");
        }
    }
}
