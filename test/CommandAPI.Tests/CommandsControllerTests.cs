using Xunit;
using Moq;
using src.CommandAPI.Data;
using System.Collections.Generic;
using src.CommandAPI.Models;
using System;
using src.CommandAPI.Controllers;
using src.CommandAPI.Profiles;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using src.CommandAPI.Dtos;

namespace test.CommandAPI.Tests
{
    public class CommandsControllerTests: IDisposable
    {
        Mock<ICommandAPIRepo> mockRepo;
        IMapper mapper;
        CommandsProfile profile;
        MapperConfiguration configuration;

        public CommandsControllerTests()
        {
            mockRepo = new Mock<ICommandAPIRepo>();
            profile = new CommandsProfile();
            configuration  = new MapperConfiguration(c => c.AddProfile(profile));
            mapper = new Mapper(configuration);
        }

        [Fact]
        public void GetCommandItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            mockRepo.Setup(r => r.GetAllCommands()).Returns(GetCommands(0));
            
            var controller = new CommandsController(mockRepo.Object, mapper);

            var result = controller.GetAllCommands();

            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDto>;

            Assert.Empty(commands);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetCommandItems_ReturnsOneResource_WhenDBHasOneItem()
        {
            mockRepo.Setup(r => r.GetAllCommands()).Returns(GetCommands(1));
            
            var controller = new CommandsController(mockRepo.Object, mapper);

            var result = controller.GetAllCommands();

            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDto>;

            Assert.Single(commands);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        private IEnumerable<Command> GetCommands(int num)
        {
            var commands = new List<Command>();

            for (int i = 1; i <= num; i++)
            {
                commands.Add(new Command {
                    Id = i,
                    HowTo = "test",
                    Platform = "test"
                });
            }

            return commands;
        }
 
 
        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            profile = null;
            configuration = null;
        }

    }
}