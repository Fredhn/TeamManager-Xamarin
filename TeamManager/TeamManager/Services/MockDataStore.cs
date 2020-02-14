using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManager.Models;

namespace TeamManager.Services
{
    public class MockDataStore : IDataStore<Employee>
    {
        readonly List<Employee> items;

        public MockDataStore()
        {
            items = new List<Employee>()
            {
                new Employee { ID = 1, FirstName = "FirstName", LastName="Last Name" },
                new Employee { ID = 2, FirstName = "FirstName",LastName="Last Name" },
                new Employee { ID = 3, FirstName = "FirstName", LastName="Last Name" },
                new Employee { ID = 4, FirstName = "FirstName",LastName="Last Name" },
                new Employee { ID = 5, FirstName = "FirstName", LastName="Last Name" },
                new Employee { ID = 6, FirstName = "FirstName", LastName="Last Name" }
            };
        }

        public async Task<bool> AddItemAsync(Employee item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Employee item)
        {
            var oldItem = items.Where((Employee arg) => arg.ID == item.ID).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Employee arg) => arg.ID == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Employee> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<Employee>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}