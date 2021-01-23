using System;
using src.CommandAPI.Models;
using Xunit;

namespace test.CommandAPI.Tests
{
    public class CommandTest: IDisposable
    {
        Command testCommand;

        public CommandTest()
        {
            testCommand = new Command{ HowTo = "Do something awesome", Platform = "xUnit", CommandLine = "dotnet test" };
        }

        [Fact]
        public void CanChangeHowTo()
        {  
            testCommand.HowTo = "Execute Unit Tests";

            Assert.Equal("Execute Unit Tests", testCommand.HowTo);
        }

        [Fact]
        public void CanChangePlatform()
        {
            testCommand.Platform = "dotnet";
   
            Assert.Equal("dotnet", testCommand.Platform);
        }

        [Fact]
        public void CanChangeCommandLine()
        {
            testCommand.CommandLine = "dotnet run";

            Assert.Equal("dotnet run", testCommand.CommandLine);
        }

        public void Dispose()
        {
            testCommand = null;
        }
    }
}