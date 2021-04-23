using Microsoft.EntityFrameworkCore;

namespace HotChocolateWithEfDemo.Domain
{
    public class ProjectsDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Developer> Developers { get; set; }

        public ProjectsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .ToTable("projects", schema: "public");
            
            modelBuilder.Entity<Project>()
                .HasKey(p => p.Id);
            
            modelBuilder.Entity<Project>()
                .Property(p => p.Id)
                .HasColumnName("id");
            modelBuilder.Entity<Project>()
                .Property(p => p.Name)
                .HasColumnName("name");


            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tasks)
                .WithOne((string) null)
                .IsRequired()
                .HasForeignKey("project_id");

            
            modelBuilder.Entity<ProjectTask>()
                .ToTable("project_tasks", schema: "public");
            modelBuilder.Entity<ProjectTask>()
                .HasKey(t => t.Id);
            
            modelBuilder.Entity<ProjectTask>()
                .Property(p => p.Id)
                .HasColumnName("id");
            modelBuilder.Entity<ProjectTask>()
                .Property(p => p.Description)
                .HasColumnName("description");
            modelBuilder.Entity<ProjectTask>()
                .Property(p => p.HoursWork)
                .HasColumnName("hours_work");
            modelBuilder.Entity<ProjectTask>()
                .Property(p => p.Cost)
                .HasColumnName("cost");
            modelBuilder.Entity<ProjectTask>()
                .Property(p => p.Status)
                .HasConversion<string>()
                .HasColumnName("status");
            modelBuilder.Entity<ProjectTask>()
                .HasOne(p => p.Assignee)
                .WithMany((string)null)
                .HasForeignKey("assignee_id");

            
            
            modelBuilder.Entity<Developer>()
                .ToTable("developers", schema: "public");
            modelBuilder.Entity<Developer>()
                .HasKey(t => t.Id);
            
            modelBuilder.Entity<Developer>()
                .Property(p => p.Id)
                .HasColumnName("id");
            modelBuilder.Entity<Developer>()
                .Property(p => p.Name)
                .HasColumnName("name");
            modelBuilder.Entity<Developer>()
                .Property(p => p.HourlyRate)
                .HasColumnName("rate");
        }
    }
}