using System;
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
using System.Windows.Shapes;

namespace SQLiteApp
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditCompany.xaml
    /// </summary>
    public partial class AddOrEditCompanyWindow : Window
    {
        public Company Company { get; private set; }

        public AddOrEditCompanyWindow(Company c)
        {
            InitializeComponent();
            Company = c;
           
            this.DataContext = Company;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

    }
}
