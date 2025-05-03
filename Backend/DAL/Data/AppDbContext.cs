using System;
using System.Collections.Generic;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public partial class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int,
    IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Master> Masters { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderService> OrderServices { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=AutoServiceBD;Username=postgres;Password=1");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<IdentityUserClaim<int>>();
        modelBuilder.Ignore<IdentityUserLogin<int>>();
        modelBuilder.Ignore<IdentityUserToken<int>>();
        modelBuilder.Ignore<IdentityRoleClaim<int>>();

        modelBuilder.Entity<User>(b =>
        {
            b.ToTable("Users");
            b.Property(u => u.Email).IsRequired();
            b.HasIndex(u => u.Email).IsUnique();
            b.Property(u => u.PhoneNumber).IsRequired();
            b.Property(u => u.UserName).IsRequired();
            b.Ignore(u => u.AccessFailedCount);
            b.Ignore(u => u.LockoutEnabled);
            b.Ignore(u => u.LockoutEnd);
            b.Ignore(u => u.TwoFactorEnabled);
            b.Ignore(u => u.EmailConfirmed);
            b.Ignore(u => u.PhoneNumberConfirmed);
            b.Ignore(u => u.SecurityStamp);
            b.Ignore(u => u.ConcurrencyStamp);
        });

        modelBuilder.Entity<IdentityRole<int>>(b =>
        {
            b.ToTable("Roles");
        });

        modelBuilder.Entity<IdentityUserRole<int>>(b =>
        {
            b.HasKey(ur => new { ur.UserId, ur.RoleId });
            b.ToTable("UserRoles");
        });
        modelBuilder.Entity<Car>(entity =>
        {
            entity.ToTable("Cars");

            entity.Property(e => e.Id)
                .HasColumnName("CarID ")
                .ValueGeneratedNever();

            entity.Property(e => e.Brand)
                .HasColumnType("character varying")
                .HasColumnName("Brand ");
            entity.Property(e => e.LicensePlate)
                .HasColumnType("character varying")
                .HasColumnName("LicensePlate ");
            entity.Property(e => e.Model)
                .HasColumnType("character varying")
                .HasColumnName("Model ");
            entity.Property(e => e.Vin)
                .HasColumnType("character varying")
                .HasColumnName("VIN ");
            entity.Property(e => e.Year)
                .HasColumnType("character varying")
                .HasColumnName("Year ");

            entity.HasOne(d => d.Client)
                .WithMany(p => p.Cars)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ClientFk");
        });

        modelBuilder.Entity<Master>(entity =>
        {
            entity.ToTable("Masters ");

            entity.Property(e => e.Id)
                .ValueGeneratedNever();

            entity.Property(e => e.Specialization)
                .HasColumnType("character varying")
                .HasColumnName("Specialization ");

            entity.HasOne(d => d.User)
                .WithOne(p => p.Master)
                .HasForeignKey<Master>(d => d.Id)
                .HasConstraintName("UserFK");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");

            entity.Property(e => e.Id)
                .HasColumnName("OrderID ")
                .ValueGeneratedNever();

            entity.Property(e => e.OrderDate).HasColumnName("OrderDate ");
            entity.Property(e => e.Status)
                .HasColumnType("character varying")
                .HasColumnName("Status ");
            entity.Property(e => e.TotalCost).HasColumnName("TotalCost ");

            entity.HasOne(d => d.Car)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CarFK");

            entity.HasOne(d => d.Client)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ClientFK");

            entity.HasOne(d => d.Master)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.MasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MasterFK");

            entity.HasOne(d => d.Payment)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("PaymentFK");
        });

        modelBuilder.Entity<OrderService>(entity =>
        {
            entity.ToTable("OrderServices");

            entity.Property(e => e.Id).HasColumnName("OrderServiceID ");
            entity.Property(e => e.Quantity).HasColumnName("Quantity ");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID ");
            entity.Property(e => e.TotalPrice).HasColumnName("TotalPrice ");

            entity.HasOne(d => d.Order)
                .WithMany(p => p.OrderServices)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderFK");

            entity.HasOne(d => d.Service)
                .WithMany(p => p.OrderServices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("ServiceFK");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payments");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("PaymentID");

            entity.Property(e => e.PaymentDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.PaymentMethod).HasColumnType("character varying");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Reviews");

            entity.Property(e => e.Id)
                .HasColumnName("ReviewID")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("CURRENT_DATE");

            entity.HasOne(d => d.Client)
                .WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("Client_FK");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedules");

            entity.Property(e => e.Id)
                .HasColumnName("ScheduleID")
                .HasDefaultValueSql("nextval('weeklyschedule_scheduleid_seq'::regclass)");

            entity.Property(e => e.WeekDay)
                .HasDefaultValueSql("'Monday'::character varying")
                .HasColumnType("character varying");

            entity.HasOne(d => d.Master)
                .WithMany(p => p.Schedules)
                .HasForeignKey(d => d.MasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MasterFK");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Services ");

            entity.Property(e => e.Id).HasColumnName("ServiceID ");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("Description ");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("Name ");
            entity.Property(e => e.Price).HasColumnName("Price ");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
