using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AddStaffingWindow.xaml
    /// </summary>
    public partial class AddStaffingWindow : Window
    {
        public Staffing NewStaffing { get; } = new Staffing();
        public ObservableCollection<Department> Departments { get; }
        public ObservableCollection<Position> Positions { get; }

        public AddStaffingWindow(
            ObservableCollection<Department> departments,
            ObservableCollection<Position> positions)
        {
            InitializeComponent();
            Departments = departments;
            Positions = positions;
            DataContext = NewStaffing;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
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