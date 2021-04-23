using System.Linq;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolateWithEfDemo.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HotChocolateWithEfDemo.GraphqlApi
{
    [ExtendObjectType("Query")]
    public class ProjectQuery
    {
        [UsePaging]
        [UseProjection]
        [HotChocolate.Data.UseFiltering]
        [HotChocolate.Data.UseSorting]
        public IQueryable<Project> Projects([FromServices] ProjectsDbContext db) =>
            db.Projects;
        
        
        public IQueryable<ProjectTask> ProjectTasks([FromServices] ProjectsDbContext db) =>
            db.ProjectTasks;
    }
}