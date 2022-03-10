using Microsoft.EntityFrameworkCore;
using DMD.Models;

namespace DMD.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Task> Tasks { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Models.Task>().HasMany(c => c.Subtasks).WithOne(c => c.ParentTask)
                .HasForeignKey(c => c.ParentID);
        }
       
    }
}
