using System;
using Microsoft.EntityFrameworkCore;

namespace ChatBot2000.Infrastructure.Ef
{
    public class AppDataContext : DbContext
    {
        public AppDataContext()
        { }

        public AppDataContext(DbContextOptions options) : base(options)
        { }
    }
}
