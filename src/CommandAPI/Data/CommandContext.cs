using Microsoft.EntityFrameworkCore;
using src.CommandAPI.Models;

namespace src.CommandAPI.Data
{
    public class CommandContext: DbContext
    {
        public DbSet<Command> Commands { get; set; }

        public CommandContext(DbContextOptions<CommandContext> options) : base (options)
        {
            
        }
        
    }
}