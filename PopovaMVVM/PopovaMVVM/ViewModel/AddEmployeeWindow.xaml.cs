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
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        public Employee NewEmployee { get; private set; }
        public ObservableCollection<Position> Positions { get; private set; }

        public AddEmployeeWindow(ObservableCollection<Position> positions)
        {
            InitializeComponent();
            Positions = positions;
            PositionComboBox.ItemsSource = Positions;
            BirthDatePicker.SelectedDate = DateTime.Now.AddYears(-30);
            AppointmentDatePicker.SelectedDate = DateTime.Now;
            GenderComboBox.SelectedIndex = 0; // default to "M"
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(MiddleNameTextBox.Text) ||
                !BirthDatePicker.SelectedDate.HasValue ||
                !AppointmentDatePicker.SelectedDate.HasValue ||
                !decimal.TryParse(SalaryTextBox.Text, out decimal salary) ||
                PositionComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill out all fields correctly.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            var selectedPosition = (Position)PositionComboBox.SelectedItem;

            NewEmployee = new Employee
            {
                EmployeeId = 0, // will assign ID when adding to collection
                LastName = LastNameTextBox.Text,
                FirstName = FirstNameTextBox.Text,
                MiddleName = MiddleNameTextBox.Text,
                Gender = gender,
                BirthDate = BirthDatePicker.SelectedDate.Value,
                AppointmentDate = AppointmentDatePicker.SelectedDate.Value,
                Salary = salary,
                Position = selectedPosition,
                Children = new System.Collections.ObjectModel.ObservableCollection<Child>()
            };

            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}