using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PopovaMVVM.Model;
using PopovaMVVM.View;
using static System.Net.Mime.MediaTypeNames;
using System.Windows;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System.IO;

namespace PopovaMVVM.ViewModel
{
    class ClassViewModel : ObservableObject
    {
        // Коллекции данных
        public ObservableCollection<Employee> Employees { get; } = new ObservableCollection<Employee>();
        public ObservableCollection<Position> Positions { get; } = new ObservableCollection<Position>();
        public ObservableCollection<Department> Departments { get; } = new ObservableCollection<Department>();
        public ObservableCollection<AdditionalPayment> AdditionalPayments { get; } = new ObservableCollection<AdditionalPayment>();
        public ObservableCollection<Staffing> Staffing { get; } = new ObservableCollection<Staffing>();

        // Выбранные элементы
        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedEmployeeChildren));
            }
        }
        public ObservableCollection<Child> SelectedEmployeeChildren => SelectedEmployee?.Children;

        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged();
            }
        }

        private Position _selectedPosition;
        public Position SelectedPosition
        {
            get => _selectedPosition;
            set
            {
                _selectedPosition = value;
                OnPropertyChanged();
            }
        }

        private Staffing _selectedStaffing;
        public Staffing SelectedStaffing
        {
            get => _selectedStaffing;
            set
            {
                _selectedStaffing = value;
                OnPropertyChanged();
            }
        }

        private AdditionalPayment _selectedPayment;
        public AdditionalPayment SelectedPayment
        {
            get => _selectedPayment;
            set
            {
                _selectedPayment = value;
                OnPropertyChanged();
            }
        }

        // Текущая вкладка
        private int _currentTabIndex;
        public int CurrentTabIndex
        {
            get => _currentTabIndex;
            set
            {
                _currentTabIndex = value;
                OnPropertyChanged();
            }
        }

        // Команды
        public RelayCommand AddEmployeeCommand { get; }
        public RelayCommand DeleteEmployeeCommand { get; }
        public RelayCommand EditEmployeeCommand { get; }
        public RelayCommand SaveEmployeeCommand { get; }

        public RelayCommand AddPositionCommand { get; }
        public RelayCommand DeletePositionCommand { get; }
        public RelayCommand EditPositionCommand { get; }
        public RelayCommand SavePositionCommand { get; }

        public RelayCommand AddDepartmentCommand { get; }
        public RelayCommand DeleteDepartmentCommand { get; }
        public RelayCommand EditDepartmentCommand { get; }
        public RelayCommand SaveDepartmentCommand { get; }

        public RelayCommand AddStaffingCommand { get; }
        public RelayCommand DeleteStaffingCommand { get; }
        public RelayCommand EditStaffingCommand { get; }
        public RelayCommand SaveStaffingCommand { get; }

        public RelayCommand AddPaymentCommand { get; }
        public RelayCommand DeletePaymentCommand { get; }
        public RelayCommand EditPaymentCommand { get; }
        public RelayCommand SavePaymentCommand { get; }

        public RelayCommand ExitCommand { get; }

        public ClassViewModel()
        {
            // Инициализация команд
            AddEmployeeCommand = new RelayCommand(_ => AddEmployee());
            DeleteEmployeeCommand = new RelayCommand(_ => DeleteEmployee());
            EditEmployeeCommand = new RelayCommand(_ => EditEmployee());
            SaveEmployeeCommand = new RelayCommand(_ => SaveEmployees());

            AddPositionCommand = new RelayCommand(_ => AddPosition());
            DeletePositionCommand = new RelayCommand(_ => DeletePosition());
            EditPositionCommand = new RelayCommand(_ => EditPosition());
            SavePositionCommand = new RelayCommand(_ => SavePositions());

            AddDepartmentCommand = new RelayCommand(_ => AddDepartment());
            DeleteDepartmentCommand = new RelayCommand(_ => DeleteDepartment());
            EditDepartmentCommand = new RelayCommand(_ => EditDepartment());
            SaveDepartmentCommand = new RelayCommand(_ => SaveDepartments());

            AddStaffingCommand = new RelayCommand(_ => AddStaffing());
            DeleteStaffingCommand = new RelayCommand(_ => DeleteStaffing());
            EditStaffingCommand = new RelayCommand(_ => EditStaffing());
            SaveStaffingCommand = new RelayCommand(_ => SaveStaffings());

            AddPaymentCommand = new RelayCommand(_ => AddPayment());
            DeletePaymentCommand = new RelayCommand(_ => DeletePayment());
            EditPaymentCommand = new RelayCommand(_ => EditPayment());
            SavePaymentCommand = new RelayCommand(_ => SavePayments());

            ExitCommand = new RelayCommand(_ => System.Windows.Application.Current.Shutdown());

            LoadData();
        }

        // Загрузка данных из настроек приложения
        public void LoadData()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.AppData))
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<StoredData>(Properties.Settings.Default.AppData);
                    if (data != null)
                    {
                        // Очищаем коллекции перед загрузкой
                        Employees.Clear();
                        Positions.Clear();
                        Departments.Clear();
                        AdditionalPayments.Clear();
                        Staffing.Clear();

                        // Загружаем данные
                        Employees.AddRange(data.Employees ?? Enumerable.Empty<Employee>());
                        Positions.AddRange(data.Positions ?? Enumerable.Empty<Position>());
                        Departments.AddRange(data.Departments ?? Enumerable.Empty<Department>());
                        AdditionalPayments.AddRange(data.AdditionalPayments ?? Enumerable.Empty<AdditionalPayment>());
                        Staffing.AddRange(data.Staffing ?? Enumerable.Empty<Staffing>());

                        LinkNavigationProperties();
                        return; // Выходим, данные загружены
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
                    // В случае ошибок — запускаем дефолтные данные
                }
            }
            // Если данных нет или произошла ошибка — инициализация дефолтных данных
            InitializeDefaultData();
        }

        // Сохранение данных в настройки приложения
        public void SaveData()
        {
            try
            {
                var data = new StoredData
                {
                    Employees = Employees.ToList(),
                    Positions = Positions.ToList(),
                    Departments = Departments.ToList(),
                    AdditionalPayments = AdditionalPayments.ToList(),
                    Staffing = Staffing.ToList()
                };

                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                };

                Properties.Settings.Default.AppData = JsonConvert.SerializeObject(data, Formatting.Indented, settings);
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        // Инициализация тестовыми данными
        private void InitializeDefaultData()
        {
            Departments.Clear();
            Positions.Clear();
            Employees.Clear();
            Staffing.Clear();
            AdditionalPayments.Clear();

            var departments = new[]
            {
                new Department { DepartmentId = 1, Name = "IT-отдел" },
                new Department { DepartmentId = 2, Name = "Финансовый отдел" },
                new Department { DepartmentId = 3, Name = "Отдел продаж" },
                new Department { DepartmentId = 4, Name = "Отдел кадров" },
                new Department { DepartmentId = 5, Name = "Техническая поддержка" }
            };
            Departments.AddRange(departments);

            var positions = new[]
            {
                new Position { PositionId = 1, Title = "Разработчик", DepartmentId = 1 },
                new Position { PositionId = 2, Title = "Бухгалтер", DepartmentId = 2 },
                new Position { PositionId = 3, Title = "Менеджер продаж", DepartmentId = 3 },
                new Position { PositionId = 4, Title = "HR-специалист", DepartmentId = 4 },
                new Position { PositionId = 5, Title = "Технический специалист", DepartmentId = 5 }
            };
            Positions.AddRange(positions);

            // Связи отделов и позиций
            foreach (var position in positions)
            {
                var dept = departments.First(d => d.DepartmentId == position.DepartmentId);
                dept.Positions = new ObservableCollection<Position>();
                dept.Positions.Add(position);
            }

            // Создаем сотрудников
            var employees = new[]
            {
                new Employee
                {
                    EmployeeId = 1,
                    LastName = "Иванов",
                    FirstName = "Иван",
                    MiddleName = "Иванович",
                    Gender = "М",
                    BirthDate = new DateTime(1985, 5, 15),
                    AppointmentDate = new DateTime(2010, 3, 1),
                    Salary = 100000,
                    PositionId = 1,
                    Children = new ObservableCollection<Child>
                    {
                        new Child { ChildId=1, FullName="Иванова Мария Ивановна", BirthDate=new DateTime(2012,3,24) },
                        new Child { ChildId=2, FullName="Иванов Петр Иванович", BirthDate=new DateTime(2015,7,12) }
                    }
                },
                new Employee
                {
                    EmployeeId = 2,
                    LastName = "Петрова",
                    FirstName = "Ольга",
                    MiddleName = "Сергеевна",
                    Gender = "Ж",
                    BirthDate = new DateTime(1990, 8, 22),
                    AppointmentDate = new DateTime(2015, 6, 10),
                    Salary = 90000,
                    PositionId = 2,
                    Children = new ObservableCollection<Child>
                    {
                        new Child { ChildId=3, FullName="Петрова Анна Сергеевна", BirthDate=new DateTime(2018,4,5) }
                    }
                },
                new Employee
                {
                    EmployeeId = 3,
                    LastName = "Сидоров",
                    FirstName = "Алексей",
                    MiddleName = "Петрович",
                    Gender = "М",
                    BirthDate = new DateTime(1988, 11, 5),
                    AppointmentDate = new DateTime(2018, 2, 15),
                    Salary = 120000,
                    PositionId = 3,
                    Children = new ObservableCollection<Child>()
                },
                new Employee
                {
                    EmployeeId=4,
                    LastName="Кузнецова",
                    FirstName="Екатерина",
                    MiddleName="Андреевна",
                    Gender="Ж",
                    BirthDate=new DateTime(1992,3,18),
                    AppointmentDate=new DateTime(2019,7,22),
                    Salary=95000,
                    PositionId=4,
                    Children=new ObservableCollection<Child>
                    {
                        new Child { ChildId=4, FullName="Кузнецов Дмитрий Иванович", BirthDate=new DateTime(2017,9,3) },
                        new Child { ChildId=5, FullName="Кузнецова София Ивановна", BirthDate=new DateTime(2020,1,15) }
                    }
                },
                new Employee
                {
                    EmployeeId=5,
                    LastName="Васильев",
                    FirstName="Дмитрий",
                    MiddleName="Николаевич",
                    Gender="М",
                    BirthDate=new DateTime(1987,6,30),
                    AppointmentDate=new DateTime(2017,4,10),
                    Salary=85000,
                    PositionId=5,
                    Children=new ObservableCollection<Child>()
                }
            };
            Employees.AddRange(employees);

            // Штатное расписание
            var staffing = new[]
            {
                new Staffing { StaffingId=1, DepartmentId=1, PositionId=1, UnitsCount=5, Salary=100000 },
                new Staffing { StaffingId=2, DepartmentId=2, PositionId=2, UnitsCount=3, Salary=90000 },
                new Staffing { StaffingId=3, DepartmentId=3, PositionId=3, UnitsCount=4, Salary=120000 },
                new Staffing { StaffingId=4, DepartmentId=4, PositionId=4, UnitsCount=2, Salary=95000 },
                new Staffing { StaffingId=5, DepartmentId=5, PositionId=5, UnitsCount=6, Salary=85000 }
            };
            Staffing.AddRange(staffing);

            // Дополнительные выплаты
            var payments = new[]
            {
                new AdditionalPayment { PaymentId=1, PositionId=1, Amount=20000, Description="Премия за проект" },
                new AdditionalPayment { PaymentId=2, PositionId=2, Amount=15000, Description="Годовая премия" },
                new AdditionalPayment { PaymentId=3, PositionId=3, Amount=25000, Description="Бонус за выполнение плана" },
                new AdditionalPayment { PaymentId=4, PositionId=4, Amount=10000, Description="Надбавка за квалификацию" },
                new AdditionalPayment { PaymentId=5, PositionId=5, Amount=12000, Description="Премия за качество обслуживания" }
            };
            AdditionalPayments.AddRange(payments);

            LinkNavigationProperties();
        }

        private void LinkNavigationProperties()
        {
            // Связь сотрудников с позициями
            foreach (var emp in Employees)
            {
                emp.Position = Positions.FirstOrDefault(p => p.PositionId == emp.PositionId);
            }

            // Связь штатных с отделами и позициями
            foreach (var s in Staffing)
            {
                s.Department = Departments.FirstOrDefault(d => d.DepartmentId == s.DepartmentId);
                s.Position = Positions.FirstOrDefault(p => p.PositionId == s.PositionId);
            }

            // Связь выплат с позициями
            foreach (var pay in AdditionalPayments)
            {
                pay.Position = Positions.FirstOrDefault(p => p.PositionId == pay.PositionId);
            }

            // Связь позиций с отделами
            foreach (var pos in Positions)
            {
                pos.Department = Departments.FirstOrDefault(d => d.DepartmentId == pos.DepartmentId);
            }
        }

        // CRUD методы
        private void AddEmployee()
        {
            var addWindow = new AddEmployeeWindow(Positions);
            addWindow.Owner = System.Windows.Application.Current.MainWindow;

            if (addWindow.ShowDialog() == true)
            {
                var newEmployee = addWindow.NewEmployee;
                newEmployee.EmployeeId = Employees.Any() ? Employees.Max(e => e.EmployeeId) + 1 : 1;
                Employees.Add(newEmployee);
                SaveData(); // сохраняем сразу после добавления
            }
        }

        private void DeleteEmployee()
        {
            if (SelectedEmployee != null)
            {
                Employees.Remove(SelectedEmployee);
                SelectedEmployee = null;
                SaveData(); // сохраняем после удаления
            }
        }

        private void EditEmployee()
        {
            if (SelectedEmployee != null)
            {
                MessageBox.Show($"Редактирование сотрудника: {SelectedEmployee.FullName}");
            }
        }

        public void SaveEmployees()
        {
            SaveData(); // сохраняем изменения
            MessageBox.Show("Данные сотрудников сохранены");
        }

        private void AddPosition()
        {
            var newPos = new Position
            {
                PositionId = Positions.Count > 0 ? Positions.Max(p => p.PositionId) + 1 : 1,
                Title = "Новая должность",
                Department = Departments.FirstOrDefault()
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
            if (SelectedPosition != null)
            {
                MessageBox.Show($"Редактирование должности: {SelectedPosition.Title}");
            }
        }

        private void SavePositions()
        {
            SaveData();
            MessageBox.Show("Должности сохранены");
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
            if (SelectedDepartment != null)
                MessageBox.Show($"Редактирование отдела: {SelectedDepartment.Name}");
        }

        private void SaveDepartments()
        {
            SaveData();
            MessageBox.Show("Отделы сохранены");
        }

        private void AddStaffing()
        {
            var newStaffing = new Staffing
            {
                StaffingId = Staffing.Count > 0 ? Staffing.Max(s => s.StaffingId) + 1 : 1,
                Department = Departments.FirstOrDefault(),
                Position = Positions.FirstOrDefault(),
                UnitsCount = 1,
                Salary = 30000
            };
            Staffing.Add(newStaffing);
            SelectedStaffing = newStaffing;
        }

        private void DeleteStaffing()
        {
            if (SelectedStaffing != null)
            {
                Staffing.Remove(SelectedStaffing);
                SelectedStaffing = null;
            }
        }

        private void EditStaffing()
        {
            if (SelectedStaffing != null)
                MessageBox.Show("Редактирование штатной единицы");
        }

        private void SaveStaffings()
        {
            SaveData();
            MessageBox.Show("Штатное расписание сохранено");
        }

        private void AddPayment()
        {
            var newPayment = new AdditionalPayment
            {
                PaymentId = AdditionalPayments.Count > 0 ? AdditionalPayments.Max(p => p.PaymentId) + 1 : 1,
                Position = Positions.FirstOrDefault(),
                Amount = 0,
                Description = "Новая выплата"
            };
            AdditionalPayments.Add(newPayment);
            SelectedPayment = newPayment;
        }

        private void DeletePayment()
        {
            if (SelectedPayment != null)
            {
                AdditionalPayments.Remove(SelectedPayment);
                SelectedPayment = null;
            }
        }

        private void EditPayment()
        {
            if (SelectedPayment != null)
                MessageBox.Show($"Редактирование выплаты: {SelectedPayment.Description}");
        }

        private void SavePayments()
        {
            SaveData();
            MessageBox.Show("Выплаты сохранены");
        }
    }

    // Вспомогательные классы
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }

    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
