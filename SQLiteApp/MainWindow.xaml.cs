﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLiteApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new CompanyViewModel();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Company company = CompaniesGrid.SelectedItem as Company;
            EmployeesWindow employeesWindow = new EmployeesWindow(company.Id);
            employeesWindow.ShowDialog();
           

            // this is used for debugging and testing.
         
        }
    }
}
