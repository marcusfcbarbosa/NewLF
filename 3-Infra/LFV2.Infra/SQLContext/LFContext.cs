using FluentValidator;
using LFV2.Domain.PedidosContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace LFV2.Infra.SQLContext
{
    public class LFContext : DbContext
    {
        private readonly IConfiguration _config;
        public LFContext()
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }
        public LFContext(DbContextOptions<LFContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public DbSet<Pedido> Pedidos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LFContext).Assembly);
            modelBuilder.Ignore<Notifiable>();
            modelBuilder.Ignore<Notification>();

            EntityMapping(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        private void EntityMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("tb_Pedidos").HasKey(e => e.Id);
                entity.Property(e => e.CreateAt).HasColumnName("createdAt").HasDefaultValue(DateTime.Now);
                entity.Property(e => e.UpdateAt).HasColumnName("updateAt");
                entity.Property(e => e.Nome)
                    .HasMaxLength(45)
                    .HasColumnName("nome");

                entity.Property(e => e.Obs)
                    .HasMaxLength(200)
                    .HasColumnName("obs");
            });
        }
    }
}
