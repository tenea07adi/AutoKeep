using Core.Entities.Persisted;
using SQLite;

namespace Persistence.DataBase
{
    public static class SqliteDataBase
    {
        private static string DbFileName { get; } = "AutoKeepSQLite.db3";

        private static SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        private static SQLiteAsyncConnection database = null;

        public static SQLiteAsyncConnection DataBase 
        {
            get 
            { 
                if(database == null)
                {
                    Init();
                }

                return database!;
            }
        }

        private static void Init()
        {
            var databasePath = Path.Combine(
                        Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData),
                        DbFileName
                        );

            database = new SQLiteAsyncConnection(databasePath, Flags);

            database.CreateTableAsync<Car>();
        }
    }
}
