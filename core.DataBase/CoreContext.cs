using core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace core.DataBase
{
    public class CoreContext : DbContext
    {
        public CoreContext(DbContextOptions<CoreContext> options)
            : base(options)
        { }

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Books> Books { get; set; }
    }
}
