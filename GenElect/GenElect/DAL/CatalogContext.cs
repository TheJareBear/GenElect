using GenElect.Models;
using System.Data.Entity;

namespace GenElect.DAL
{
    public class CatalogContext:DbContext
    {
        public DbSet<Elective> Electives { get; set; }
        public DbSet<Period> Periods { get; set; }
    }
}