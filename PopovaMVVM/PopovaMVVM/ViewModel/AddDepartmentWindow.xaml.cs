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
using PopovaMVVM.Model;

namespace PopovaMVVM.ViewModel
{
    /// <summary>
    /// Логика взаимодействия для AddDepartmentWindow.xaml
    /// </summary>
    public partial class AddDepartmentWindow : Window
    {
        public Department NewDepartment { get; } = new Department();

        public AddDepartmentWindow()
        {
            InitializeComponent();
            DataContext = NewDepartment;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewDepartment.Name))
            {
                MessageBox.Show("Please enter a department name.");
                return;
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}