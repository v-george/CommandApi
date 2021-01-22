
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using src.CommandAPI.Data;
using src.CommandAPI.Models;

namespace src.CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo repo;
        public CommandsController(ICommandAPIRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> Get()
        {
            return Ok(repo.GetAllCommands());
        }

        [HttpGet("id")]
        public ActionResult<Command> GetCommandById(int id)
        {
            var command = repo.GetCommandById(id);

            if(command == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(command);
            }
        }
    } 
}