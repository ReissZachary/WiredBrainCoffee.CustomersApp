using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.Base;

namespace WiredBrainCoffee.CustomersApp.Model
{
    class Customer : Observable
    {
        private string firstname;
        private string lastName;
        private bool isDeveloper;

        public string Firstname
        {
            get => firstname;
            set
            {
                firstname = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }
        public bool IsDeveloper
        {
            get => isDeveloper;
            set
            {
                isDeveloper = value;
                OnPropertyChanged();
            }
        }


    }
}
