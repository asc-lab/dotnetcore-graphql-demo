using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using HotChocolateWithEfDemo.Domain;
using HotChocolateWithEfDemo.GraphqlApi.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateWithEfDemo.GraphqlApi
{
    [ExtendObjectType("Mutation")]
    public class ProjectMutations
    {
        public async Task<AddProjectPayload> AddProject(AddProjectInput input, [FromServices] ProjectsDbContext db)
        {
            if (db.Projects.Any(p => p.Name == input.Name))
            {
                return new AddProjectPayload(new[] {new UserError("Not unique project name", "ERR01")});
            }
            
            var newProject = new Project(input.Name);

            db.Projects.Add(newProject);

            await db.SaveChangesAsync();

            return new AddProjectPayload(newProject);
        }
        
        public async Task<AddTaskPayload> AddTask(AddTaskInput input, [FromServices] ProjectsDbContext db,  [Service]ITopicEventSender eventSender)
        {
            try
            {
                var project = await db.Projects.SingleAsync(p => p.Name == input.ProjectName);
                project.AddTask(input.TaskDescription);

                await db.SaveChangesAsync();

                var addedTask = project.Task(input.TaskDescription);

                await eventSender.SendAsync(nameof(TaskSubscriptions.OnTaskAdded), addedTask.Id);

                return new AddTaskPayload(addedTask);
            }
            catch (ApplicationException ex)
            {
                return new AddTaskPayload(new[] {new UserError(ex.Message, "ERR01")});
            }
        }
    }
    
    public record AddProjectInput(string Name);

    public class ProjectPayloadBase : Payload
    {
        protected ProjectPayloadBase(Project project)
        {
            Project = project;
        }

        protected ProjectPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Project Project { get; }
    }
    
    public class AddProjectPayload : ProjectPayloadBase
    {
        public AddProjectPayload(Project project) : base(project)
        {
            
        }
        
        public AddProjectPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
            
        }
    }
    
    public class TaskPayloadBase : Payload
    {
        protected TaskPayloadBase(ProjectTask task)
        {
            Task = task;
        }

        protected TaskPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public ProjectTask Task { get; }
    }
    
    public record AddTaskInput(string ProjectName, string TaskDescription);

    public class AddTaskPayload : TaskPayloadBase
    {
        public AddTaskPayload(ProjectTask task) : base(task)
        {
        }
        
        public AddTaskPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
            
        }
    }
}