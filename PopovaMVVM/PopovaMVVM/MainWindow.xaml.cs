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
using Microsoft.Win32;
using PopovaMVVM.ViewModel;

namespace PopovaMVVM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AppDbContext _context;
        private readonly ReportGenerator _reportGenerator;

        public MainWindow()
        {
            InitializeComponent();

            _context = new AppDbContext();
            _reportGenerator = new ReportGenerator(_context);

            DataContext = new ClassViewModel();
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            (DataContext as ClassViewModel)?.SaveData();
        }

        // Новая кнопка для генерации всех отчетов в одном файле
        private void GenerateAllReports_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                FileName = "Все_отчеты.xlsx"
            };

            if (saveDialog.ShowDialog() == true)
            {
                try
                {
                    _reportGenerator.GenerateAllReportsInOneFile(saveDialog.FileName);
                    MessageBox.Show("Все отчеты успешно созданы в одном файле!", "Успех",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка генерации отчетов",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        
    }
}