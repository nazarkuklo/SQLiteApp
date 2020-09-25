using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SQLiteApp
{
    class CompanyViewModel : INotifyPropertyChanged
    {
        ApplicationContext db;
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        IEnumerable<Company> companies;

        public IEnumerable<Company> Companies
        {
            get { return companies; }
            set
            {
                companies = value;
                OnPropertyChanged("Companies");
            }
        }

        public CompanyViewModel()
        {
            db = new ApplicationContext();
            db.Companies.Load();

            Companies = db.Companies.Local.ToBindingList();
        }

        
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      AddOrEditCompanyWindow companyWindow = new AddOrEditCompanyWindow(new Company());
                      if (companyWindow.ShowDialog() == true)
                      {
                          Company company = companyWindow.Company;
                          db.Companies.Add(new Company() { Name = company.Name.Trim(), Address=company.Address.Trim(), 
                              Description=company.Description.Trim(), PhoneNumber = company.PhoneNumber.Trim() });
                          db.SaveChanges();
                      }
                      
                  }));
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Company company = selectedItem as Company;

                        Company vm = new Company()
                        {
                            Id = company.Id,
                            Name = company.Name,
                            Description = company.Description,
                            Address = company.Address,
                            PhoneNumber = company.PhoneNumber
                        };
                        AddOrEditCompanyWindow companyWindow = new AddOrEditCompanyWindow(vm);

                        if (companyWindow.ShowDialog() == true)
                        {
                            company = db.Companies.Find(companyWindow.Company.Id);
                            if (company != null)
                            {
                                company.Name = companyWindow.Company.Name.Trim();
                                company.Description = companyWindow.Company.Description.Trim();
                                company.Address = companyWindow.Company.Address.Trim();
                                company.PhoneNumber = companyWindow.Company.PhoneNumber.Trim();
                                db.Entry(company).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Company company = selectedItem as Company;
                        
                        if (company.Employees.Any())
                        {
                            if (MessageBox.Show("Do you really want to delete this company. All employees of this company will be removed !!!!", 
                                "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                            {
                                return;
                            }
                            else
                            {
                                db.Employees.RemoveRange(company.Employees);
                                db.Companies.Remove(company);
                                db.SaveChanges();
                            }

                        }
                        else
                        {
                            db.Companies.Remove(company);
                            db.SaveChanges();
                        }
                        
                      
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
