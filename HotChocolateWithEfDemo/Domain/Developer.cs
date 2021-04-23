using System;

namespace HotChocolateWithEfDemo.Domain
{
    public class Developer
    {
        public Guid? Id { get; private init; }
        public string Name { get; private set; }
        public decimal HourlyRate { get; private set; }

        public Developer(string name, decimal hourlyRate)
        {
            Id = Guid.NewGuid();
            Name = name;
            HourlyRate = hourlyRate;
        }

        protected Developer()
        {
        }
    }
}