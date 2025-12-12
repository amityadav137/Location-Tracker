using SQLite;

namespace LocationTracker
{
    public class LocationDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public LocationDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<LocationEntry>().Wait();
        }

        public Task<int> SaveLocationAsync(LocationEntry entry) => _database.InsertAsync(entry);

        public Task<List<LocationEntry>> GetLocationsAsync() => _database.Table<LocationEntry>().ToListAsync();
    }
}
