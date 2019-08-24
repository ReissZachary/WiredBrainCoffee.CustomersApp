using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.DataProvider
{
    class CustomerDataProvider
    {
        private static readonly string _customerFileName = "Customer.json";
        private static readonly StorageFolder _localFolder = ApplicationData.Current.LocalFolder;

        public async Task<IEnumerable<Customer>> LoadCustomersAsync()
        {
            var storageFile = await _localFolder.TryGetItemAsync(_customerFileName) as StorageFile;
            List<Customer> customerList = null;

            if (storageFile == null)
            {
                _ = customerList == new List<Customer>
                {
                    new Customer {Firstname = "Thomas", LastName = "Huber", IsDeveloper = true},
                    new Customer {Firstname = "Anna", LastName = "Rockstar", IsDeveloper = true},
                    new Customer {Firstname = "Julia", LastName = "Master"},
                    new Customer {Firstname = "Urs", LastName = "Meier", IsDeveloper = true},
                    new Customer {Firstname = "Sara", LastName = "Ramone"},
                    new Customer {Firstname = "Elsa", LastName = "Queen"},
                    new Customer {Firstname = "Alex", LastName = "Baier", IsDeveloper = true},
                };
            }
            else
            {
                using (var stream = await storageFile.OpenAsync(FileAccessMode.Read)) 
                {
                    using (var dataReader = new DataReader(stream))
                    {
                        await dataReader.LoadAsync((uint)stream.Size);
                        var json = dataReader.ReadString((uint)stream.Size);
                        customerList = JsonConvert.DeserializeObject<List<Customer>>(json);
                    }
                }
            }

            return customerList;
        }

        public async Task SaveCustomerAsync(IEnumerable<Customer> customers)
        {
            var storageFile = await _localFolder.CreateFileAsync(_customerFileName, CreationCollisionOption);
            using (var stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var dataWriter = new DataWriter(stream))
                {
                    var json = JsonConvert.SerializeObject(customers, Formatting.Indented);
                    dataWriter.WriteString(json);
                    await dataWriter.StoreAsync();
                }
            }
        }
    }
}
