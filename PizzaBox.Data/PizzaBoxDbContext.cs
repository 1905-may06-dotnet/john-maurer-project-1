using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaBox.Data
{
    public partial class PizzaBoxDbContext : DbContext
    {
        public PizzaBoxDbContext()
        {
        }

        public PizzaBoxDbContext(DbContextOptions<PizzaBoxDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Entities.Address> Addresses { get; set; }
        public virtual DbSet<Entities.Employee> Employees { get; set; }
        public virtual DbSet<Entities.Feature> Features { get; set; }
        public virtual DbSet<Entities.Item> Items { get; set; }
        public virtual DbSet<Entities.Order> Orders { get; set; }
        public virtual DbSet<Entities.Outlet> Outlets { get; set; }
        public virtual DbSet<Entities.Person> People { get; set; }
        public virtual DbSet<Entities.StateTax> StateTaxes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=model;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Entities.Address>(entity =>
            {
                entity.HasIndex(e => e.OutletId)
                    .HasName("IX_FK_OutletAddress");

                entity.HasIndex(e => e.PersonId)
                    .HasName("IX_FK_PersonAddress");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Outlet)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.OutletId)
                    .HasConstraintName("FK_OutletAddress");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_PersonAddress");
            });

            modelBuilder.Entity<Entities.Employee>(entity =>
            {
                entity.HasIndex(e => e.OutletId)
                    .HasName("IX_FK_EmployeeOutlet");

                entity.HasIndex(e => e.PersonId)
                    .HasName("IX_FK_EmployeePerson");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Outlet)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.OutletId)
                    .HasConstraintName("FK_EmployeeOutlet");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeePerson");
            });

            modelBuilder.Entity<Entities.Feature>(entity =>
            {
                entity.HasIndex(e => e.OutletId)
                    .HasName("IX_FK_OutletFeature");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Outlet)
                    .WithMany(p => p.Features)
                    .HasForeignKey(d => d.OutletId)
                    .HasConstraintName("FK_OutletFeature");
            });

            modelBuilder.Entity<Entities.Item>(entity =>
            {
                entity.HasIndex(e => e.OutletId)
                    .HasName("IX_FK_OutletItem");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Outlet)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.OutletId)
                    .HasConstraintName("FK_OutletItem");
            });

            modelBuilder.Entity<Entities.Order>(entity =>
            {
                entity.HasIndex(e => e.OutletId)
                    .HasName("IX_FK_OutletOrder");

                entity.HasIndex(e => e.PersonId)
                    .HasName("IX_FK_PersonOrder");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Outlet)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OutletId)
                    .HasConstraintName("FK_OutletOrder");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_PersonOrder");
            });

            modelBuilder.Entity<Entities.Outlet>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Entities.Person>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Entities.StateTax>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}