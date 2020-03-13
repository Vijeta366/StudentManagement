using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext() : base("StudentManagement") { }
        public DbSet<Class> Class { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Attendence> Attendence { get; set; }

    }
}