using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)   
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
    .HasOne(x => x.Education);
            ;
            modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
            modelBuilder.Entity<Education>().HasData(
                new Education { Id=1, Name = "Grad" },
                new Education { Id=2, Name = "Undergrad" }
                );
            modelBuilder.Entity<Role>().HasData(
               new Role { Id=1, Name = "Admin" },
               new Role { Id=2, Name = "NormalUser" }
               );
        }
    }
}
