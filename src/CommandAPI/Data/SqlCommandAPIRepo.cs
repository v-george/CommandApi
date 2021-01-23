using System;
using System.Collections.Generic;
using System.Linq;
using src.CommandAPI.Models;

namespace src.CommandAPI.Data
{
    public class SqlCommandAPIRepo : ICommandAPIRepo
    {
        private readonly CommandContext context;
        public SqlCommandAPIRepo(CommandContext context)
        {
            this.context = context;

        }
        public void CreateCommand(Command command)
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            context.Add(command);
        }

        public void DeleteCommand(Command command)
        {
            if(command == null)
            {
                throw new ArgumentNullException();
            }
            context.Commands.Remove(command);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return context.Commands.FirstOrDefault(c => c.Id == id);
        }

        public bool SaveChanges()
        {
            var result = context.SaveChanges();
            return result >= 0;
        }

        public void UpdateCommand(Command command)
        {
        
        }
    }
}