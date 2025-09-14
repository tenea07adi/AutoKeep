using Core.Entities.Persisted;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
    }
}
