using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TeamManager.Models;
using TeamManager.Repository;

namespace TeamManager.Data
{
    public class TeamManagerDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public RepoEmployee _employee;
        public RepoSpentVacations _spentVacations;

        public TeamManagerDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Employee>().Wait();
            _database.CreateTableAsync<SpentVacations>().Wait();

            _employee = new RepoEmployee(dbPath);
            _spentVacations = new RepoSpentVacations(dbPath);
        }
    }
}
