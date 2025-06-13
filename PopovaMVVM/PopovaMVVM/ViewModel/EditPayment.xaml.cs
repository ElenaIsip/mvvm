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
    /// Логика взаимодействия для EditPayment.xaml
    /// </summary>
    public partial class EditPayment : Window
    {
        public AdditionalPayment Payment { get; }
        public ObservableCollection<Position> Positions { get; }

        public EditPayment(AdditionalPayment payment, ObservableCollection<Position> positions)
        {
            InitializeComponent();
            Payment = payment;
            Positions = positions;
            DataContext = Payment;
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