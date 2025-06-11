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

    public class AdditionalPaymentViewModel : ButtonViewModel
    {
        private const string DataFilePath = "additional_payments.json";

        public ObservableCollection<AdditionalPayment> Payments { get; set; }
        public AdditionalPayment SelectedPayment { get; set; }

        public AdditionalPaymentViewModel()
        {
            Payments = new ObservableCollection<AdditionalPayment>();
            LoadPayments();

            AddButtonCommand = new RelayCommand(_ => AddPayment());
            DeleteButtonCommand = new RelayCommand(_ => DeletePayment(), _ => SelectedPayment != null);
            EditButtonCommand = new RelayCommand(_ => EditPayment(), _ => SelectedPayment != null);
            SaveButtonCommand = new RelayCommand(_ => SavePayments());
        }

        private void LoadPayments()
        {
            if (File.Exists(DataFilePath))
            {
                try
                {
                    var json = File.ReadAllText(DataFilePath);
                    var list = JsonConvert.DeserializeObject<List<AdditionalPayment>>(json);
                    if (list != null)
                    {
                        foreach (var item in list)
                            Payments.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
                }
            }
        }

        private void SavePayments()
        {
            try
            {
                var list = Payments.ToList();
                var json = JsonConvert.SerializeObject(list, Formatting.Indented);
                File.WriteAllText(DataFilePath, json);
                Console.WriteLine("Данные успешно сохранены");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

     
        private void AddPayment()
        {
            var newPayment = new AdditionalPayment
            {
                PaymentId = Payments.Count > 0 ? Payments.Max(p => p.PaymentId) + 1 : 1,
                PositionId = 1,
                Amount = 1000,
                Description = "Премия"
            };
            Payments.Add(newPayment);
            SelectedPayment = newPayment;
        }

        private void DeletePayment()
        {
            if (SelectedPayment != null)
            {
                Payments.Remove(SelectedPayment);
                SelectedPayment = null;
            }
        }

        private void EditPayment()
        {
            
        }

        protected override void AddData() => AddPayment();

        protected override void DeleteData() => DeletePayment();

        protected override void EditData() => EditPayment();

        protected override void SaveData() => SavePayments();
    }
}

