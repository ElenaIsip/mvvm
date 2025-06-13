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
    /// Логика взаимодействия для EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Window
    {
        public Employee SelectedEmployee { get; }
        public ObservableCollection<Position> Positions { get; }

        public EditEmployee(Employee selectedEmployee, ObservableCollection<Position> positions)
        {
            InitializeComponent();
            SelectedEmployee = selectedEmployee;
            Positions = positions;
            DataContext = SelectedEmployee; // Устанавливаем контекст данных
        }

        private void AddChildButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedEmployee.Children.Add(new Child());
        }

        private void DeleteChildButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Child child)
            {
                SelectedEmployee.Children.Remove(child);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
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
