
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using src.CommandAPI.Data;
using src.CommandAPI.Dtos;
using src.CommandAPI.Models;

namespace src.CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo repo;
        private readonly IMapper mapper;
        public CommandsController(ICommandAPIRepo repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> Get()
        {
            var commands = repo.GetAllCommands();
            return Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var command = repo.GetCommandById(id);

            if (command == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<CommandReadDto>(command));
            }
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto command)
        {
            var commandModel = mapper.Map<Command>(command);
            repo.CreateCommand(commandModel);
            repo.SaveChanges();

            var CommandReadDto = mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new {Id =  CommandReadDto.Id}, CommandReadDto);
        
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto command)
        {
            var commandModel = repo.GetCommandById(id);
            if(commandModel == null)
            {
                return NotFound();
            }            
            mapper.Map(command, commandModel );
            repo.UpdateCommand(commandModel);
            repo.SaveChanges();

            return NoContent();       
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var command = repo.GetCommandById(id);

            if(command == null)
            {
                return NotFound();
            }

            repo.DeleteCommand(command);
            repo.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModel = repo.GetCommandById(id);
            if(commandModel == null)
            {
                return NotFound();
            } 

            var commandToPatch = mapper.Map<CommandUpdateDto>(commandModel);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            mapper.Map(commandToPatch, commandModel);
            repo.UpdateCommand(commandModel);
            repo.SaveChanges();

            return NoContent();

        }
    }
}