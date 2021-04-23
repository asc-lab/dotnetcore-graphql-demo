using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotChocolateWithEfDemo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateWithEfDemo.GraphqlApi
{
    [ExtendObjectType("Subscription")]
    public class TaskSubscriptions
    {
        [Subscribe]
        [Topic]
        public Task<ProjectTask> OnTaskAdded([EventMessage] Guid? id,[FromServices] ProjectsDbContext db, CancellationToken cancellationToken)
        {
            return db.ProjectTasks.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }
    }
}