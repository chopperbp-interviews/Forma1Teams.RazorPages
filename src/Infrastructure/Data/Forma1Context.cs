using Forma1Teams.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Forma1Teams.Infrastructure.Data
{
    public class Forma1Context : DbContext
    {
        public Forma1Context(DbContextOptions<Forma1Context> options) : base(options)
        {
        }
        public DbSet<Team> Teams { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            /*builder.Entity<Team>(a =>
                a.Property();
            */
        }
    }
}
