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
    public class PositionViewModel : ButtonViewModel
    {
        private const string DataFilePath = "positions.json";

        public ObservableCollection<Position> Positions { get; set; }
        public Position SelectedPosition { get; set; }

        public PositionViewModel()
        {
            Positions = new ObservableCollection<Position>();
            LoadPositions();

            AddButtonCommand = new RelayCommand(_ => AddPosition());
            DeleteButtonCommand = new RelayCommand(_ => DeletePosition(), _ => SelectedPosition != null);
            EditButtonCommand = new RelayCommand(_ => EditPosition(), _ => SelectedPosition != null);
            SaveButtonCommand = new RelayCommand(_ => SavePositions());
        }

        
        private void LoadPositions()
        {
            if (File.Exists(DataFilePath))
            {
                try
                {
                    var json = File.ReadAllText(DataFilePath);
                    var list = JsonConvert.DeserializeObject<ObservableCollection<Position>>(json);
                    if (list != null)
                        Positions = list;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
                }
            }
        }

       
        private void SavePositions()
        {
            try
            {
                var json = JsonConvert.SerializeObject(Positions, Formatting.Indented);
                File.WriteAllText(DataFilePath, json);
                Console.WriteLine("Данные успешно сохранены");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        
        private void AddPosition()
        {
            var newPos = new Position
            {
                PositionId = Positions.Count > 0 ? Positions.Max(p => p.PositionId) + 1 : 1,
                Title = "Должность",
                DepartmentId = 1
            };
            Positions.Add(newPos);
            SelectedPosition = newPos;
        }

       
        private void DeletePosition()
        {
            if (SelectedPosition != null)
            {
                Positions.Remove(SelectedPosition);
                SelectedPosition = null;
            }
        }

       
        private void EditPosition()
        {
          
        }

        
        protected override void AddData() => AddPosition();
        protected override void DeleteData() => DeletePosition();
        protected override void EditData() => EditPosition();
        protected override void SaveData() => SavePositions();
    }
}