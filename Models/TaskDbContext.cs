using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace taskmanagementapi.Models
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {

        }
        public DbSet<Task> tasks { get; set; }
        public DbSet<Users> users { get; set; }

    }
}
