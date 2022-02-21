#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlphaBetaWebApplication.Models;

namespace AlphaBetaWebApplication.Data
{
    public class AlphaBetaWebApplicationContext : DbContext
    {
        public AlphaBetaWebApplicationContext (DbContextOptions<AlphaBetaWebApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<AlphaBetaWebApplication.Models.User> User { get; set; }
    }
}
