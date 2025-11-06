using Core.Entities.Abstract;
using Core.Entities.Persisted;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Car> Cars { get; set; }

        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<GenericReminder> GenericReminders { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasDiscriminator<string>("vehicle_type")
                .HasValue<Vehicle>("vehicle_base")
                .HasValue<Car>("vehicle_car");

            modelBuilder.Entity<Reminder>()
                .HasDiscriminator<string>("reminder_type")
                .HasValue<Reminder>("reminder_base")
                .HasValue<GenericReminder>("reminder_generic");
        }
    }
}
