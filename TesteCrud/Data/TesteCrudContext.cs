using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TesteCrud.Models;

namespace TesteCrud.Data
{
    public class TesteCrudContext : DbContext
    {
        public TesteCrudContext (DbContextOptions<TesteCrudContext> options)
            : base(options)
        {
        }

        public DbSet<TesteCrud.Models.Contato> Contato { get; set; }
    }
}
