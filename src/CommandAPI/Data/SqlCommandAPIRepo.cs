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
            if(command.Id == 0)
            {
                context.Add(command);
            }
            else
            {
                UpdateCommand(command);
            }
            
            SaveChanges();
        }

        public void DeleteCommand(Command command)
        {
            context.Remove(command);
            SaveChanges();
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
            return result == 1;
        }

        public void UpdateCommand(Command command)
        {
            context.Entry(command).GetDatabaseValues().SetValues(command);
            SaveChanges();
        }
    }
}