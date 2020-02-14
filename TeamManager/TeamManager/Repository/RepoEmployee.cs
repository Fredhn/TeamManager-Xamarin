using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Models;

namespace TeamManager.Repository
{
    public class RepoEmployee
    {
        readonly SQLiteAsyncConnection _database;

        public RepoEmployee(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            //_database.CreateTableAsync<Employee>().Wait();
        }

        public Task<List<Employee>> GetEmployeesAsync()
        {
            return _database.Table<Employee>().ToListAsync();
        }

        public Task<Employee> GetEmployeeAsync(int id)
        {
            return _database.Table<Employee>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveEmployeeAsync(Employee employee)
        {
            if (employee.ID != 0)
            {
                return _database.UpdateAsync(employee);
            }
            else
            {
                return _database.InsertAsync(employee);
            }
        }

        public Task<int> DeleteEmployeeAsync(Employee employee)
        {
            return _database.DeleteAsync(employee);
        }
    }
}
