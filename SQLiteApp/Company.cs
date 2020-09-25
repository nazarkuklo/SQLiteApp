using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SQLiteApp
{
   public class Company : INotifyPropertyChanged, IDataErrorInfo
    {
        private string name;
        private string description;
        private string address;
        private string phoneNumber;
        public int Id { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public Company()
        {
            Employees = new List<Employee>();
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrWhiteSpace(Name))
                        {
                            error = "The field is requiered";
                        }
                        break;
                    case "Description":
                        if (string.IsNullOrWhiteSpace(Description))
                        {
                            error = "The field is requiered";
                        }
                        break;
                    case "Address":
                        if (string.IsNullOrWhiteSpace(Address))
                        {
                            error = "The field is requiered";
                        }
                        break;
                    case "PhoneNumber":
                        if (string.IsNullOrWhiteSpace(PhoneNumber))
                        {
                            error = "The field is requiered";
                        }
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
