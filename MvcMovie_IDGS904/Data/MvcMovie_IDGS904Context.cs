using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie_IDGS904.Models;

namespace MvcMovie_IDGS904.Data
{
    public class MvcMovie_IDGS904Context : DbContext
    {
        public MvcMovie_IDGS904Context (DbContextOptions<MvcMovie_IDGS904Context> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie_IDGS904.Models.Movie> Movie { get; set; } = default!;
    }
}
