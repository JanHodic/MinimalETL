using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MinimalETL.Server.Models;

namespace MinimalETL.Server.Data.Contexts
{
    public class TestETLDbContext : DbContext
    {          
        public DbSet<Item> Items { get; set; }
        public TestETLDbContext(DbContextOptions<TestETLDbContext> options) : base(options)
        {
        }
    }
}

