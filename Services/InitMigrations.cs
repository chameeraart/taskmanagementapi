using taskmanagementapi.Models;
using Microsoft.EntityFrameworkCore;

namespace taskmanagementapi.Services
{
    public class InitMigrations
    {
        private readonly TaskDbContext context;
        public InitMigrations(TaskDbContext context)
        {
            this.context = context;
        }
        public void MigrateDatabase()
        {
            context.Database.Migrate();
        }
    }
}
