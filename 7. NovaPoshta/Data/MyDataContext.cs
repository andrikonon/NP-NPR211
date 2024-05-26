using _7._NovaPoshta.Constants;
using _7._NovaPoshta.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace _7._NovaPoshta.Data;

public class MyDataContext : DbContext
{
    public DbSet<AreaEntity> Areas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(AppDatabase.ConnectionString);
    }
}