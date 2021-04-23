using System;
using System.Collections.Generic;
using System.Linq;
using HotChocolate;

namespace HotChocolateWithEfDemo.Domain
{
    public class Project
    {
        public Guid? Id { get; private init; }
        public string Name { get; private set; }
        public virtual List<ProjectTask> Tasks { get; private init; }

        public Project(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Tasks = new ();
        }

        protected Project()
        {
        }

        public void AddTask(string desc)
        {
            if (Tasks.Any(t => t.Description == desc))
                throw new ApplicationException($"Project already contains task with description {desc}");
            
            var task = new ProjectTask(desc);
            Tasks.Add(task);
        }


        [GraphQLIgnore]
        public ProjectTask Task(string desc) => Tasks.FirstOrDefault(t => t.Description == desc);
    }

    public enum ProjectTaskStatus
    {
        Open,
        InProgress,
        Done
    }
}