using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Models;

namespace TeamManager.Repository
{
    public class RepoSpentVacations
    {
        readonly SQLiteAsyncConnection _database;

        public RepoSpentVacations(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            //_database.CreateTableAsync<SpentVacations>().Wait();
        }

        public Task<List<SpentVacations>> GetSpentVacationsAsync()
        {
            return _database.Table<SpentVacations>().ToListAsync();
        }

        public Task<List<SpentVacations>> GetSpentVacationsAsync(int id)
        {
            return _database.Table<SpentVacations>()
                            .Where(i => i.IDEmployee == id)
                            .ToListAsync();
        }

        public Task<List<SpentVacations>> GetSpentVacationsPeriodsAsync(int IdEmployee, int refYear)
        {
            return _database.Table<SpentVacations>()
                            .Where(i => i.IDEmployee == IdEmployee && i.RefYear == refYear)
                            .ToListAsync();
        }

        public Task<int> SaveSpentVacationsAsync(SpentVacations spentVacations)
        {
            if (spentVacations.ID != 0)
            {
                return _database.UpdateAsync(spentVacations);
            }
            else
            {
                return _database.InsertAsync(spentVacations);
            }
        }

        public Task<int> DeleteSpentVacationsAsync(SpentVacations spentVacations)
        {
            return _database.DeleteAsync(spentVacations);
        }
    }
}
