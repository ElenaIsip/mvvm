using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using PopovaMVVM.Model;
using PopovaMVVM.ViewModel;

namespace PopovaMVVM.View
{
    public class EmployeeViewModel : ButtonViewModel
    {
        private const string DataFilePath = "employees.json";

        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Position> Positions { get; set; } // Коллекция должностей
        public Employee SelectedEmployee { get; set; }

        public RelayCommand AddButtonCommand { get; }
        public RelayCommand DeleteButtonCommand { get; }
        public RelayCommand EditButtonCommand { get; }
        public RelayCommand SaveButtonCommand { get; }

        public EmployeeViewModel()
        {
            Employees = new ObservableCollection<Employee>();
            Positions = new ObservableCollection<Position>();
            LoadEmployees();

            // Инициализация команд
            AddButtonCommand = new RelayCommand(_ => AddEmployee());
            DeleteButtonCommand = new RelayCommand(_ => DeleteEmployee(), _ => SelectedEmployee != null);
            EditButtonCommand = new RelayCommand(_ => EditEmployee(), _ => SelectedEmployee != null);
            SaveButtonCommand = new RelayCommand(_ => SaveEmployees());

            LoadPositions(); 
        }

        private void LoadPositions()
        {
           
            Positions.Add(new Position { PositionId = 1, Title = "Engineer" });
            Positions.Add(new Position { PositionId = 2, Title = "Manager" });
            Positions.Add(new Position { PositionId = 3, Title = "Director" });
        }

        private void LoadEmployees()
        {
            if (File.Exists(DataFilePath))
            {
                try
                {
                    var json = File.ReadAllText(DataFilePath);
                    var list = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(json);
                    if (list != null)
                        Employees = list;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading data: {ex.Message}");
                }
            }
        }

        private void SaveEmployees()
        {
            try
            {
                var json = JsonConvert.SerializeObject(Employees, Formatting.Indented);
                File.WriteAllText(DataFilePath, json);
                Console.WriteLine("Data saved successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        private void AddEmployee()
        {
            
            var addWindow = new AddEmployeeWindow(Positions);
            addWindow.Owner = Application.Current.MainWindow;

            if (addWindow.ShowDialog() == true)
            {
                var newEmployee = addWindow.NewEmployee;
               
                newEmployee.EmployeeId = Employees.Any() ? Employees.Max(e => e.EmployeeId) + 1 : 1;
                Employees.Add(newEmployee);
            }
        }

        private void DeleteEmployee()
        {
            if (SelectedEmployee != null)
            {
                Employees.Remove(SelectedEmployee);
                SelectedEmployee = null;
            }
        }

        private void EditEmployee()
        {
           
        }

        protected override void AddData() => AddEmployee();
        protected override void DeleteData() => DeleteEmployee();
        protected override void EditData() => EditEmployee();
        protected override void SaveData() => SaveEmployees();
    }
}