using IND.klasser;
using Microsoft.EntityFrameworkCore;

public class SchoolContext : DbContext
{
    public DbSet<Personal> Personals { get; set; }
    public DbSet<Elev> Elevers { get; set; }
    public DbSet<Kurs> Kursers { get; set; }
    public DbSet<Betyg> Betygs { get; set; }
    public DbSet<Lon> Lons { get; set; }

    public DbSet<ElevInfo> ElevInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder.UseSqlServer("Server=DESKTOP-FVF2TLQ;Database=School;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<Personal>()
            .HasMany(p => p.Betyg)
            .WithOne(b => b.Larare)
            .HasForeignKey(b => b.LarareID);

        modelBuilder.Entity<Personal>()
            .HasMany(p => p.Lons)
            .WithOne(l => l.Personal) 
            .HasForeignKey(l => l.PersonID);

        modelBuilder.Entity<Elev>()
            .HasMany(e => e.Betyg)
            .WithOne(b => b.Elev)
            .HasForeignKey(b => b.ElevID);

        modelBuilder.Entity<Kurs>()
            .HasMany(k => k.Betyg)
            .WithOne(b => b.Kurs)
            .HasForeignKey(b => b.KursID);
        modelBuilder.Entity<ElevInfo>().HasNoKey();
    }
}