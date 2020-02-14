using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using TeamManager.Models;
using TeamManager.Views;

namespace TeamManager.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        public ObservableCollection<Employee> Employees { get; set; }
        public Command LoadItemsCommand { get; set; }

        public EmployeesViewModel()
        {
            Title = "Employees";
            Employees = new ObservableCollection<Employee>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            
            MessagingCenter.Subscribe<NewEmployeePage, Employee>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Employee;
                Employees.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            //if (IsBusy)
            //    return;

            IsBusy = true;

            try
            {
                Employees.Clear();
                var items = await App.Database._employee.GetEmployeesAsync();
                //var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Employees.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsBusy = false;
                });
                //IsBusy = false;
            }
        }
    }
}