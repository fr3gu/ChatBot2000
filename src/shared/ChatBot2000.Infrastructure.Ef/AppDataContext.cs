using System;
using ChatBot2000.Core.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ChatBot2000.Infrastructure.Ef
{
    public class AppDataContext : DbContext
    {
        public AppDataContext()
        { }

        public AppDataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<RepeatingMessage> RepeatingMessages { get; set; }
        public DbSet<SimpleResponseMessage> StaticCommandResponseMessages { get; set; }
    }
}
