using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PopovaMVVM.ViewModel;

namespace PopovaMVVM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClassViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ClassViewModel();
            DataContext = _viewModel;
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            _viewModel.SaveData();
        }
    }

}