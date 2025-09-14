using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.DataBase;

namespace Persistence.Configuration
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataBaseContext>
    {
        public DataBaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();

            optionsBuilder.UseSqlite(Path.Combine(
                        Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData),
                        "AutoKeepSQLite.db3"
                        ));

            return new DataBaseContext(optionsBuilder.Options);
        }
    }
}
