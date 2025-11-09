using BankSystem.API.model;
using Microsoft.EntityFrameworkCore;

public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options) : base(options)
    {
    }

    public DbSet<BankAccountModel> Accounts { get; set; }

    public DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<BankAccountModel>()
                    .HasKey(a => a.Id);

        modelBuilder.Entity<BankAccountModel>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Number).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Balance).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Status).IsRequired();

            entity.HasOne(c => c.Client)
                  .WithMany(a => a.Accounts)
                  .HasForeignKey(c => c.ClientId)
                 .OnDelete(DeleteBehavior.Restrict);

        });

        modelBuilder.Entity<Client>()
                    .HasKey(c => c.Id);
    }
}