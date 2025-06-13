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
    /// Логика взаимодействия для AddPaymentWindow.xaml
    /// </summary>
    public partial class AddPaymentWindow : Window
    {
        public AdditionalPayment NewPayment { get; } = new AdditionalPayment();
        public ObservableCollection<Position> Positions { get; }

        public AddPaymentWindow(ObservableCollection<Position> positions)
        {
            InitializeComponent();
            Positions = positions;
            DataContext = NewPayment;
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