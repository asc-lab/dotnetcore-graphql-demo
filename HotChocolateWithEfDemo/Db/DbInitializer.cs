using HotChocolateWithEfDemo.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolateWithEfDemo.Db
{
    public static class DbInitializer
    {
        public static void Init(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var db =  scope.ServiceProvider.GetRequiredService<ProjectsDbContext>();

            db.Database.ExecuteSqlRaw("delete from projects");
            db.Database.ExecuteSqlRaw("delete from project_tasks");
            db.Database.ExecuteSqlRaw("delete from developers");

            var mike = new Developer("Mike", 120M);
            var tim = new Developer("Tim", 100M);
            db.Developers.AddRange(new[] {mike, tim});
            
            var p1 = new Project("Falcon");
            db.Projects.Add(p1);
            p1.AddTask("Build risk module");
            p1.AddTask("Build sales module");

            var p2 = new Project("Panel");
            p2.AddTask("Build post module");
            p2.Task("Build post module").Assign(mike);
            p2.Task("Build post module").LogWork(8M);
            p2.Task("Build post module").Finish();
            db.Projects.Add(p2);
            db.SaveChanges();

        }
    }
}