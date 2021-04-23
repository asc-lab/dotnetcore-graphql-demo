using System;

namespace HotChocolateWithEfDemo.Domain
{
    public class ProjectTask
    {
        public Guid? Id { get; private init; }
        public string Description { get; private set; }
        public ProjectTaskStatus Status { get; private set; }
        public virtual Developer Assignee { get; private set; }
        public decimal Cost { get; private set; }
        public decimal HoursWork { get; private set; }
        
        //??public Project Project {get; private set;}

        public ProjectTask(string description)
        {
            Description = description;
            Status = ProjectTaskStatus.Open;
            Cost = 0M;
            HoursWork = 0M;
        }

        protected ProjectTask()
        {
        }

        public void Assign(Developer developer)
        {
            Assignee = developer;
        }

        public void Finish()
        {
            Status = ProjectTaskStatus.Done;
        }
        
        public void LogWork(decimal hours)
        {
            HoursWork += hours;
            Cost += hours * Assignee.HourlyRate;
        }
    }
}