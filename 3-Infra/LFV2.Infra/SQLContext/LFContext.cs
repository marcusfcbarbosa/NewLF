using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

        //public DbSet<AnuncioWebMotors> AnuncioWebMotors { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebMotorsContext).Assembly);
        //    modelBuilder.Ignore<Notifiable>();
        //    modelBuilder.Ignore<Notification>();

        //    EntityMapping(modelBuilder);
        //    base.OnModelCreating(modelBuilder);
        //}

    }
}
