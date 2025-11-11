using BankSystem.API.model;
using Microsoft.EntityFrameworkCore;

public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options) : base(options)
    {
    }

    public DbSet<BankAccountModel> Accounts { get; set; }

    public DbSet<ClientModel> Clients { get; set; }

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
                 .OnDelete(DeleteBehavior.Cascade);

        });

        modelBuilder.Entity<ClientModel>((entity) =>
        {

            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).ValueGeneratedOnAdd();


            entity.Property(c => c.Name).IsRequired().HasMaxLength(150);


            entity.Property(c => c.Email).IsRequired().HasMaxLength(100);
            entity.HasIndex(c => c.Email).IsUnique();


            entity.Property(c => c.Cpf).IsRequired().HasMaxLength(11);
            entity.HasIndex(c => c.Cpf).IsUnique();


            entity.Property(c => c.DateOfBirth).IsRequired();
        });



    }
}


