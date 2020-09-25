using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SQLiteApp
{
    public class Employee : INotifyPropertyChanged, IDataErrorInfo
    {
        private string firstName;
        private string lastName;
        private string phoneNumber;
        private string position;
        public int Id { get; set; }
        private int companyId { get; set; }
        public virtual Company Company { get; set; }

        public int CompanyId
        {
            get { return companyId; }
            set
            {
                companyId = value;
                OnPropertyChanged("CompanyId");
            }
        }


        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged("Position");
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
                    case "FirstName":
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            error = "The field is requiered";
                        }
                        break;
                    case "LastName":
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            error = "The field is requiered";
                        }
                        break;
                    case "Position":
                        if (string.IsNullOrWhiteSpace(Position))
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
