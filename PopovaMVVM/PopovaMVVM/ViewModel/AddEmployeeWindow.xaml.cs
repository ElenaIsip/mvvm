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
        public ObservableCollection<Child> Children { get; } = new ObservableCollection<Child>();

        public AddEmployeeWindow(ObservableCollection<Position> positions)
        {
            InitializeComponent();
            Positions = positions;
            PositionComboBox.ItemsSource = Positions;
            BirthDatePicker.SelectedDate = DateTime.Now.AddYears(-30);
            AppointmentDatePicker.SelectedDate = DateTime.Now;
            GenderComboBox.SelectedIndex = 0; // default to "М"

            // Initialize children collection
            ChildrenDataGrid.ItemsSource = Children;
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
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate children
            foreach (var child in Children)
            {
                if (//string.IsNullOrWhiteSpace(child.LastName) ||
                    //string.IsNullOrWhiteSpace(child.FirstName) ||
                    child.DateOfBirth == default)
                {
                    MessageBox.Show("Пожалуйста, заполните фамилию, имя и дату рождения для всех детей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            string gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var selectedPosition = (Position)PositionComboBox.SelectedItem;

            NewEmployee = new Employee
            {
                LastName = LastNameTextBox.Text,
                FirstName = FirstNameTextBox.Text,
                MiddleName = MiddleNameTextBox.Text,
                Gender = gender,
                BirthDate = BirthDatePicker.SelectedDate.Value,
                AppointmentDate = AppointmentDatePicker.SelectedDate.Value,
                Salary = salary,
                Position = selectedPosition,
                PositionId = selectedPosition.PositionId,
                Children = new ObservableCollection<Child>(Children)
            };

            this.DialogResult = true;
        }

        private void AddChildButton_Click(object sender, RoutedEventArgs e) =>
            // Добавляем пустого ребенка для ввода данных
            Children.Add(new Child
            {
                //LastName = "",
                //FirstName = "",
                //MiddleName = "",
                DateOfBirth = DateTime.Now
            });

        private void DeleteChildButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Child child)
            {
                Children.Remove(child);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}