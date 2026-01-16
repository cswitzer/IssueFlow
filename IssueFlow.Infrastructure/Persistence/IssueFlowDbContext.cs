using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IssueFlow.Domain.Issues;
using IssueFlow.Domain.Profiles;
using IssueFlow.Domain.Projects;
using IssueFlow.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace IssueFlow.Infrastructure.Persistence;

public class IssueFlowDbContext : IdentityDbContext<ApplicationUser>
{
    public IssueFlowDbContext(DbContextOptions<IssueFlowDbContext> options) : base(options)
    {
    }

    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<IssueType> IssueTypes { get; set; }
    public DbSet<IssueStatus> IssueStatuses { get; set; }
    public DbSet<IssuePriority> IssuePriorities { get; set; }

    private void SeedDatabase(ModelBuilder builder)
    {
        var now = DateTime.UtcNow;

        // add a range of issue priorities
        builder.Entity<IssuePriority>().HasData(
            new IssuePriority { 
                Id = SeedIds.IssuePriorities.Low, 
                Name = "Low", 
                Description = "Low priority tasks and small bug fixes",
                SortOrder = 1,
                CreatedAt = now
            },
            new IssuePriority
            { 
                Id = SeedIds.IssuePriorities.Medium, 
                Name = "Medium", 
                Description = "Tasks and issues that take precendence over Low, but not High",
                SortOrder = 2,
                CreatedAt = now
            },
            new IssuePriority
            { 
                Id = SeedIds.IssuePriorities.High, 
                Name = "High", 
                Description = "Critical tasks and blockers that need immediate attention",
                SortOrder = 3,
                CreatedAt = now
            }
        );

        builder.Entity<IssueStatus>().HasData(
            new IssueStatus { 
                Id = SeedIds.IssueStatuses.Todo, 
                Name = "To Do", 
                Description = "Issues that are yet to be started",
                IsFinal = false,
                SortOrder = 1,
                CreatedAt = now
            },
            new IssueStatus
            {
                Id = SeedIds.IssueStatuses.InProgress,
                Name = "In Progress",
                Description = "Issues that are currently being worked on",
                IsFinal = false,
                SortOrder = 2,
                CreatedAt = now
            },
            new IssueStatus
            {
                Id = SeedIds.IssueStatuses.Blocked,
                Name = "Blocked",
                Description = "Issues that are blocked and cannot proceed",
                IsFinal = false,
                SortOrder = 3,
                CreatedAt = now
            },
            new IssueStatus
            {
                Id = SeedIds.IssueStatuses.Done,
                Name = "Done",
                Description = "Issues that have been completed",
                IsFinal = true,
                SortOrder = 4,
                CreatedAt = now
            },
            new IssueStatus 
            {
                Id = SeedIds.IssueStatuses.Resolved,
                Name = "Resolved",
                Description = "Issues that are no longer active for any reason",
                IsFinal = true,
                SortOrder = 5,
                CreatedAt = now
            }
        );

        builder.Entity<IssueType>().HasData(
            new IssueType
            {
                Id = SeedIds.IssueTypes.Bug,
                Name = "Bug",
                Description = "A problem which impairs or prevents the functions of the product",
                SortOrder = 1,
                CreatedAt = now
            },
            new IssueType
            {
                Id = SeedIds.IssueTypes.Task,
                Name = "Task",
                Description = "A general task that needs to be accomplished",
                SortOrder = 2,
                CreatedAt = now
            },
            new IssueType
            {
                Id = SeedIds.IssueTypes.Story,
                Name = "Story",
                Description = "A user story that describes a feature from the end-user perspective",
                SortOrder = 3,
                CreatedAt = now
            },
            new IssueType
            {
                Id = SeedIds.IssueTypes.Epic,
                Name = "Epic",
                Description = "A large body of work that can be broken down into smaller tasks or stories",
                SortOrder = 4,
                CreatedAt = now
            }
        );
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Deleting a profile should NOT delete an entire project
        builder.Entity<Project>()
            .HasOne(project => project.OwnerProfile)
            .WithMany(profile => profile.OwnedProjects)
            .HasForeignKey(project => project.OwnerProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        // Deleting priorities, statuses, and types should NOT delete any issues
        builder.Entity<Issue>()
            .HasOne(i => i.IssuePriority)
            .WithMany()
            .HasForeignKey(i => i.IssuePriorityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Issue>()
            .HasOne(i => i.IssueStatus)
            .WithMany()
            .HasForeignKey(i => i.IssueStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Issue>()
            .HasOne(i => i.IssueType)
            .WithMany()
            .HasForeignKey(i => i.IssueTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Deleting a project should remove all the issues assigned to that project
        builder.Entity<Issue>()
            .HasOne(i => i.Project)
            .WithMany(p => p.Issues)
            .HasForeignKey(i => i.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        // No two Issues can have the same Issue.ProjectId and Issue.Key. Make this indexable
        // for fast lookups by this key
        builder.Entity<Issue>()
            .HasIndex(i => new { i.ProjectId, i.Key })
            .IsUnique();

        // Seed initial data
        SeedDatabase(builder);
    }
}

// TODO: Make migrations AND use fluent validation inside of Application to decouple models from
// data validations, which is a business thing