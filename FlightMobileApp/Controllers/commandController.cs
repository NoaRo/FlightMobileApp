using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightMobileApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightMobileApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class commandController : ControllerBase
    {
        private CommandManager commandManager;
        public commandController(CommandManager manager)
        {
            commandManager = manager;
        }

        // POST: api/command
        [HttpPost]
        public ActionResult<string> SendCommand([FromBody] Command c)
        {
            if (c.Aileron<-1 || c.Aileron >1|| c.Throttle<0 || c.Throttle>1 ||
                c.Elevator<-1 || c.Elevator>1 || c.Rudder<-1 || c.Rudder > 1)
            {
                return BadRequest();
            } 
            commandManager.SendCommand(c);
            return Ok();
        }
    }
}
