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
    }
}

// TODO: Make migrations AND use fluent validation inside of Application to decouple models from
// data validations, which is a business thing