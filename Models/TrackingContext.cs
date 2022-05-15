using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace TrackingApi.Models
{
    public class TrackingContext : DbContext
    {
        public TrackingContext(DbContextOptions<TrackingContext> options)
            : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; } = null!;
    }
}
