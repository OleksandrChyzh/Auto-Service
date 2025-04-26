using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Master> Masters { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderService> OrderServices { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Warrantye> Warrantyes { get; set; }

    public virtual DbSet<Weeklyschedule> Weeklyschedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=AutoServiceBD;Username=postgres;Password=1");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("Car _pkey");

            entity.HasOne(d => d.Client).WithMany(p => p.Cars).HasConstraintName("ClientFK");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("Client _pkey");

            entity.HasOne(d => d.User).WithMany(p => p.Clients).HasConstraintName("UserFK");
        });

        modelBuilder.Entity<Master>(entity =>
        {
            entity.HasKey(e => e.MasterId).HasName("Master _pkey");

            entity.HasOne(d => d.User).WithMany(p => p.Masters).HasConstraintName("UserFK");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("Orders_pkey");

            entity.HasOne(d => d.Car).WithMany(p => p.Orders).HasConstraintName("CarFK");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders).HasConstraintName("ClientFK");

            entity.HasOne(d => d.Master).WithMany(p => p.Orders).HasConstraintName("MasterFK");
        });

        modelBuilder.Entity<OrderService>(entity =>
        {
            entity.HasKey(e => e.OrderServiceId).HasName("OrderService _pkey");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderServices).HasConstraintName("OrderFK");

            entity.HasOne(d => d.Service).WithMany(p => p.OrderServices).HasConstraintName("ServiceFK");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Paymentid).HasName("payment_pkey");

            entity.Property(e => e.Paymentid).HasDefaultValueSql("nextval('payment_paymentid_seq'::regclass)");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_payment");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("Service _pkey");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.Shiftid).HasName("shift_pkey");

            entity.Property(e => e.Shiftid).HasDefaultValueSql("nextval('shift_shiftid_seq'::regclass)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.Property(e => e.Userid).HasDefaultValueSql("nextval('users_userid_seq'::regclass)");
            entity.Property(e => e.Createdat).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<Warrantye>(entity =>
        {
            entity.HasKey(e => e.Warrantyid).HasName("warranty_pkey");

            entity.Property(e => e.Warrantyid).HasDefaultValueSql("nextval('warranty_warrantyid_seq'::regclass)");

            entity.HasOne(d => d.Order).WithMany(p => p.Warrantyes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_warranty");
        });

        modelBuilder.Entity<Weeklyschedule>(entity =>
        {
            entity.HasKey(e => e.Scheduleid).HasName("weeklyschedule_pkey");

            entity.Property(e => e.Scheduleid).HasDefaultValueSql("nextval('weeklyschedule_scheduleid_seq'::regclass)");

            entity.HasOne(d => d.Master).WithMany(p => p.Weeklyschedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_master");

            entity.HasOne(d => d.Shift).WithMany(p => p.Weeklyschedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shift");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
