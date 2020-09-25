using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SQLiteApp
{
    class EmployeesViewModel : INotifyPropertyChanged
    {
        ApplicationContext db;
        RelayCommand addCommandEmployee;
        RelayCommand editCommandEmployee;
        RelayCommand deleteCommandEmployee;

        IEnumerable<Employee> employees;

        public int CompanyId { get; set; }

        public IEnumerable<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                OnPropertyChanged("Employees");
            }
        }

        public EmployeesViewModel(int Id)
        {
            CompanyId = Id;
            db = new ApplicationContext();
            db.Employees.Where(employee => employee.CompanyId == Id).Load();

            Employees = db.Employees.Local.ToBindingList();
        }


        public RelayCommand AddCommandEmployee
        {
            get
            {
                return addCommandEmployee ??
                  (addCommandEmployee = new RelayCommand((o) =>
                  {
                      AddOrEditEmployeeWindow employeeWindow = new AddOrEditEmployeeWindow(new Employee());
                      if (employeeWindow.ShowDialog() == true)
                      {
                          Employee employee = employeeWindow.Employee;
                          db.Employees.Add(new Employee()
                          {
                              FirstName = employee.FirstName.Trim(),
                              LastName = employee.LastName.Trim(),
                              Position = employee.Position.Trim(),
                              PhoneNumber = employee.PhoneNumber.Trim(),
                              CompanyId = CompanyId
                          });
                          db.SaveChanges();
                      }

                  }));
            }
        }

        public RelayCommand EditCommandEmployee
        {
            get
            {
                return editCommandEmployee ??
                    (editCommandEmployee = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Employee employee = selectedItem as Employee;

                        Employee vm = new Employee()
                        {
                            Id = employee.Id,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            Position = employee.Position,
                            PhoneNumber = employee.PhoneNumber,
                            CompanyId = employee.CompanyId
                        };
                        AddOrEditEmployeeWindow employeeWindow = new AddOrEditEmployeeWindow(vm);

                        if (employeeWindow.ShowDialog() == true)
                        {
                            employee = db.Employees.Find(employeeWindow.Employee.Id);
                            if (employee != null)
                            {
                                employee.FirstName = employeeWindow.Employee.FirstName.Trim();
                                employee.LastName = employeeWindow.Employee.LastName.Trim();
                                employee.Position = employeeWindow.Employee.Position.Trim();
                                employee.PhoneNumber = employeeWindow.Employee.PhoneNumber.Trim();
                                db.Entry(employee).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }));
            }
        }

        public RelayCommand DeleteCommandEmployee
        {
            get
            {
                return deleteCommandEmployee ??
                    (deleteCommandEmployee = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Employee employee = selectedItem as Employee;
                        db.Employees.Remove(employee);
                        db.SaveChanges();
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
