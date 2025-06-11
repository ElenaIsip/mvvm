using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PopovaMVVM.Model;

namespace PopovaMVVM.View
{
    public class DepartmentViewModel : ButtonViewModel
    {
        private const string DataFilePath = "departments.json";

        public ObservableCollection<Department> Departments { get; set; }
        public Department SelectedDepartment { get; set; }

        public DepartmentViewModel()
        {
            Departments = new ObservableCollection<Department>();
            LoadDepartments();

            AddButtonCommand = new RelayCommand(_ => AddDepartment());
            DeleteButtonCommand = new RelayCommand(_ => DeleteDepartment(), _ => SelectedDepartment != null);
            EditButtonCommand = new RelayCommand(_ => EditDepartment(), _ => SelectedDepartment != null);
            SaveButtonCommand = new RelayCommand(_ => SaveDepartments());
        }

        private void LoadDepartments()
        {
            if (File.Exists(DataFilePath))
            {
                try
                {
                    var json = File.ReadAllText(DataFilePath);
                    var list = JsonConvert.DeserializeObject<ObservableCollection<Department>>(json);
                    if (list != null)
                    {
                        Departments = list;
                    }
                }
                catch (Exception ex)
                {
                   
                    Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
                }
            }
        }

        
        private void SaveDepartments()
        {
            try
            {
                var json = JsonConvert.SerializeObject(Departments, Formatting.Indented);
                File.WriteAllText(DataFilePath, json);
                Console.WriteLine("Данные успешно сохранены");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        
        private void AddDepartment()
        {
            var newDept = new Department
            {
                DepartmentId = Departments.Count > 0 ? Departments.Max(d => d.DepartmentId) + 1 : 1,
                Name = "Новый отдел"
            };
            Departments.Add(newDept);
            SelectedDepartment = newDept;
        }

       
        private void DeleteDepartment()
        {
            if (SelectedDepartment != null)
            {
                Departments.Remove(SelectedDepartment);
                SelectedDepartment = null;
            }
        }

        
        private void EditDepartment()
        {
            
        }

        
        protected override void AddData() => AddDepartment();
        protected override void DeleteData() => DeleteDepartment();
        protected override void EditData() => EditDepartment();
        protected override void SaveData() => SaveDepartments();
    }
}


