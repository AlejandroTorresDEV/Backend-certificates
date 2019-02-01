using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GttApiWeb.Models
{
    public class JiraContext : DbContext
    {

        public JiraContext(DbContextOptions<JiraContext> options) : base(options)
        { }

        public DbSet<Jira> Users { get; set; }
    }
}
