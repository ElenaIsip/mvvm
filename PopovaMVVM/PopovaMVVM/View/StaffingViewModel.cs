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
    public class StaffingViewModel : ButtonViewModel
    {
        private const string DataFilePath = "staffings.json";

        public ObservableCollection<Staffing> Staffings { get; set; }
        public Staffing SelectedStaffing { get; set; }

        public StaffingViewModel()
        {
            Staffings = new ObservableCollection<Staffing>();
            LoadStaffings();

            AddButtonCommand = new RelayCommand(_ => AddStaffing());
            DeleteButtonCommand = new RelayCommand(_ => DeleteStaffing(), _ => SelectedStaffing != null);
            EditButtonCommand = new RelayCommand(_ => EditStaffing(), _ => SelectedStaffing != null);
            SaveButtonCommand = new RelayCommand(_ => SaveStaffings());
        }

        private void LoadStaffings()
        {
            if (File.Exists(DataFilePath))
            {
                try
                {
                    var json = File.ReadAllText(DataFilePath);
                    var list = JsonConvert.DeserializeObject<ObservableCollection<Staffing>>(json);
                    if (list != null)
                        Staffings = list;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
                }
            }
        }

   
        private void SaveStaffings()
        {
            try
            {
                var json = JsonConvert.SerializeObject(Staffings, Formatting.Indented);
                File.WriteAllText(DataFilePath, json);
                Console.WriteLine("Данные успешно сохранены");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
            }
        }


        private void AddStaffing()
        {
            var newStaffing = new Staffing
            {
                StaffingId = Staffings.Count > 0 ? Staffings.Max(s => s.StaffingId) + 1 : 1,
                PositionId = 1,
                DepartmentId = 1,
                UnitsCount = 1,
                Salary = 30000
            };
            Staffings.Add(newStaffing);
            SelectedStaffing = newStaffing;
        }

        
        private void DeleteStaffing()
        {
            if (SelectedStaffing != null)
            {
                Staffings.Remove(SelectedStaffing);
                SelectedStaffing = null;
            }
        }

       
        private void EditStaffing()
        {
           
        }

        
        protected override void AddData() => AddStaffing();
        protected override void DeleteData() => DeleteStaffing();
        protected override void EditData() => EditStaffing();
        protected override void SaveData() => SaveStaffings();
    }
}

