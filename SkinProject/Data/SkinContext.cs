using Microsoft.EntityFrameworkCore;
using SkinProject.Models;

namespace SkinProject.Data;

public class SkinContext : DbContext
{
    public DbSet<Skin> Skins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=skin.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
}