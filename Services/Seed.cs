using taskmanagementapi.Models;
using static taskmanagementapi.Models.Users;
using Task = taskmanagementapi.Models.Task;

namespace taskmanagementapi.Services
{
    public class Seed
    {
        public TaskDbContext Context { get; }

        public Seed(TaskDbContext context)
        {

            Context = context;
        }

        public void SeedData()
        {

            var tasks = Context.tasks;

            if (tasks != null)
            {
                foreach (var tasklist in tasks)
                {
                    Context.tasks.Remove(tasklist);
                }

                Context.SaveChanges();
            }

            var task = new List<Task> {
                new Task {Title="chameera",Description="Test",DueDate=DateTime.Now,active=true},
                   new Task {Title="chatuska",Description="Test",DueDate=DateTime.Now,active=true},
                      new Task {Title="dinuka",Description="Test",DueDate=DateTime.Now, active = true},
            };

            Context.AddRange(task);
            Context.SaveChanges();


            var users = Context.users;

            if (users != null)
            {
                foreach (var user in users)
                {
                    Context.users.Remove(user);
                }

                Context.SaveChanges();
            }

            var userlist = new List<Users> {
                new Users {username="chameera",password="123",UserType= taskmanagementapi.Models.Users.UserTypes.Admin,isactive=true},
                   new Users {username="a",password="a",UserType= taskmanagementapi.Models.Users.UserTypes.Admin,isactive=true},
                      new Users {username="chathuska",password="123",UserType= taskmanagementapi.Models.Users.UserTypes.Admin, isactive = true},
                        new Users {username="softone",password="123",UserType= taskmanagementapi.Models.Users.UserTypes.Admin, isactive = true},
            };

            Context.AddRange(userlist);
            Context.SaveChanges();


        }
    }
}
