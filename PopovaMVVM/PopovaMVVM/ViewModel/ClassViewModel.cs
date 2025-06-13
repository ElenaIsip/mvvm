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
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace PopovaMVVM.ViewModel
{

    class ClassViewModel : ObservableObject
    {
        private readonly AppDbContext _dbContext;

        // Коллекции данных
        public ObservableCollection<Employee> Employees { get; } = new ObservableCollection<Employee>();
        public ObservableCollection<Position> Positions { get; } = new ObservableCollection<Position>();
        public ObservableCollection<Department> Departments { get; } = new ObservableCollection<Department>();
        public ObservableCollection<AdditionalPayment> AdditionalPayments { get; } = new ObservableCollection<AdditionalPayment>();

        internal void SaveData()
        {
            try
            {
                _dbContext.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения данных: {ex.Message}");
            }
        }

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
            _dbContext = new AppDbContext();

            // Инициализация команд
            AddEmployeeCommand = new RelayCommand(_ => AddEmployee());
            DeleteEmployeeCommand = new RelayCommand(_ => DeleteEmployee());
            EditEmployeeCommand = new RelayCommand(_ => EditEmployee());

            AddPositionCommand = new RelayCommand(_ => AddPosition());
            DeletePositionCommand = new RelayCommand(_ => DeletePosition());
            EditPositionCommand = new RelayCommand(_ => EditPosition());

            AddDepartmentCommand = new RelayCommand(_ => AddDepartment());
            DeleteDepartmentCommand = new RelayCommand(_ => DeleteDepartment());
            EditDepartmentCommand = new RelayCommand(_ => EditDepartment());

            AddStaffingCommand = new RelayCommand(_ => AddStaffing());
            DeleteStaffingCommand = new RelayCommand(_ => DeleteStaffing());
            EditStaffingCommand = new RelayCommand(_ => EditStaffing());

            AddPaymentCommand = new RelayCommand(_ => AddPayment());
            DeletePaymentCommand = new RelayCommand(_ => DeletePayment());
            EditPaymentCommand = new RelayCommand(_ => EditPayment());

            ExitCommand = new RelayCommand(_ => System.Windows.Application.Current.Shutdown());

            LoadData();
        }

        // Загрузка данных из базы данных
        public void LoadData()
        {
            try
            {
                // Очищаем коллекции перед загрузкой
                Employees.Clear();
                Positions.Clear();
                Departments.Clear();
                AdditionalPayments.Clear();
                Staffing.Clear();

                // Загружаем данные из базы
                _dbContext.Departments.Load();
                _dbContext.Positions.Load();
                _dbContext.Employees.Include(e => e.Children).Load();
                _dbContext.Staffing.Load();
                _dbContext.AdditionalPayments.Load();

                // Заполняем коллекции
                Employees.AddRange(_dbContext.Employees.Local.ToObservableCollection());
                Positions.AddRange(_dbContext.Positions.Local.ToObservableCollection());
                Departments.AddRange(_dbContext.Departments.Local.ToObservableCollection());
                Staffing.AddRange(_dbContext.Staffing.Local.ToObservableCollection());
                AdditionalPayments.AddRange(_dbContext.AdditionalPayments.Local.ToObservableCollection());

                // Если база пустая, инициализируем тестовыми данными
                if (!Departments.Any())
                {
                    InitializeDefaultData();
                }

                LinkNavigationProperties();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        // Инициализация тестовыми данными
        private void InitializeDefaultData()
        {
            try
            {
                // Создаем отделы
                var dept1 = new Department { Name = "IT-отдел" };
                var dept2 = new Department { Name = "Финансовый отдел" };
                var dept3 = new Department { Name = "Отдел продаж" };
                var dept4 = new Department { Name = "Отдел кадров" };
                var dept5 = new Department { Name = "Техническая поддержка" };
                _dbContext.Departments.AddRange(dept1, dept2, dept3, dept4, dept5);
                _dbContext.SaveChanges();

                // Должности
                var pos1 = new Position { Title = "Разработчик", DepartmentId = dept1.DepartmentId };
                var pos2 = new Position { Title = "Бухгалтер", DepartmentId = dept2.DepartmentId };
                var pos3 = new Position { Title = "Менеджер продаж", DepartmentId = dept3.DepartmentId };
                var pos4 = new Position { Title = "HR-специалист", DepartmentId = dept4.DepartmentId };
                var pos5 = new Position { Title = "Технический специалист", DepartmentId = dept5.DepartmentId };
                _dbContext.Positions.AddRange(pos1, pos2, pos3, pos4, pos5);
                _dbContext.SaveChanges();

                // Сотрудники
                var emp1 = new Employee
                {
                    LastName = "Иванов",
                    FirstName = "Иван",
                    MiddleName = "Иванович",
                    Gender = "М",
                    BirthDate = new DateTime(1985, 5, 15),
                    AppointmentDate = new DateTime(2010, 3, 1),
                    Salary = 100000,
                    PositionId = pos1.PositionId,
                    Children = new ObservableCollection<Child>
                    {
                        new Child { FullName = "Иванова Мария Ивановна", DateOfBirth = new DateTime(2012, 3, 24) },
                        new Child { FullName = "Иванов Петр Иванович", DateOfBirth = new DateTime(2015, 7, 12) }
                    }
                };

                var emp2 = new Employee
                {
                    LastName = "Петрова",
                    FirstName = "Ольга",
                    MiddleName = "Сергеевна",
                    Gender = "Ж",
                    BirthDate = new DateTime(1990, 8, 22),
                    AppointmentDate = new DateTime(2015, 6, 10),
                    Salary = 90000,
                    PositionId = pos2.PositionId,
                    Children = new ObservableCollection<Child>
                    {
                        new Child { FullName = "Петрова Анна Сергеевна", DateOfBirth = new DateTime(2018, 4, 5) }
                    }
                };

                var emp3 = new Employee
                {
                    LastName = "Сидоров",
                    FirstName = "Алексей",
                    MiddleName = "Петрович",
                    Gender = "М",
                    BirthDate = new DateTime(1988, 11, 5),
                    AppointmentDate = new DateTime(2018, 2, 15),
                    Salary = 120000,
                    PositionId = pos3.PositionId
                };

                var emp4 = new Employee
                {
                    LastName = "Кузнецова",
                    FirstName = "Екатерина",
                    MiddleName = "Андреевна",
                    Gender = "Ж",
                    BirthDate = new DateTime(1992, 3, 18),
                    AppointmentDate = new DateTime(2019, 7, 22),
                    Salary = 95000,
                    PositionId = pos4.PositionId,
                    Children = new ObservableCollection<Child>
                    {
                        new Child { FullName = "Кузнецов Дмитрий Иванович", DateOfBirth = new DateTime(2017, 9, 3) },
                        new Child { FullName = "Кузнецова София Ивановна", DateOfBirth = new DateTime(2020, 1, 15) }
                    }
                };

                var emp5 = new Employee
                {
                    LastName = "Васильев",
                    FirstName = "Дмитрий",
                    MiddleName = "Николаевич",
                    Gender = "М",
                    BirthDate = new DateTime(1987, 6, 30),
                    AppointmentDate = new DateTime(2017, 4, 10),
                    Salary = 85000,
                    PositionId = pos5.PositionId
                };

                _dbContext.Employees.AddRange(emp1, emp2, emp3, emp4, emp5);
                _dbContext.SaveChanges();

                // Штатное расписание
                var staffing = new[]
                {
                    new Staffing { DepartmentId = dept1.DepartmentId, PositionId = pos1.PositionId, UnitsCount = 5, Salary = 100000 },
                    new Staffing { DepartmentId = dept2.DepartmentId, PositionId = pos2.PositionId, UnitsCount = 3, Salary = 90000 },
                    new Staffing { DepartmentId = dept3.DepartmentId, PositionId = pos3.PositionId, UnitsCount = 4, Salary = 120000 },
                    new Staffing { DepartmentId = dept4.DepartmentId, PositionId = pos4.PositionId, UnitsCount = 2, Salary = 95000 },
                    new Staffing { DepartmentId = dept5.DepartmentId, PositionId = pos5.PositionId, UnitsCount = 6, Salary = 85000 }
                };
                _dbContext.Staffing.AddRange(staffing);

                // Дополнительные выплаты
                var payments = new[]
                {
                    new AdditionalPayment { PositionId = pos1.PositionId, Amount = 20000, Description = "Премия за проект" },
                    new AdditionalPayment { PositionId = pos2.PositionId, Amount = 15000, Description = "Годовая премия" },
                    new AdditionalPayment { PositionId = pos3.PositionId, Amount = 25000, Description = "Бонус за выполнение плана" },
                    new AdditionalPayment { PositionId = pos4.PositionId, Amount = 10000, Description = "Надбавка за квалификацию" },
                    new AdditionalPayment { PositionId = pos5.PositionId, Amount = 12000, Description = "Премия за качество обслуживания" }
                };
                _dbContext.AdditionalPayments.AddRange(payments);

                _dbContext.SaveChanges();

                // Обновляем данные после инициализации
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации данных: {ex.Message}");
            }
        }

        private void LinkNavigationProperties()
        {
            // Связь сотрудников с позициями
            foreach (var emp in Employees)
            {
                emp.Position = Positions.FirstOrDefault(p => p.PositionId == emp.PositionId);
            }

            // Для штатного расписания
            foreach (var s in Staffing)
            {
                s.Department = Departments.FirstOrDefault(d => d.DepartmentId == s.DepartmentId);
                s.Position = Positions.FirstOrDefault(p => p.PositionId == s.PositionId);
            }

            // Для доплат
            foreach (var pay in AdditionalPayments)
            {
                pay.Position = Positions.FirstOrDefault(p => p.PositionId == pay.PositionId);
            }

            // Для позиций
            foreach (var pos in Positions)
            {
                pos.Department = Departments.FirstOrDefault(d => d.DepartmentId == pos.DepartmentId);
            }
        }

        // CRUD методы
        private void AddEmployee()
        {
            var addWindow = new AddEmployeeWindow(Positions);
            if (addWindow.ShowDialog() == true)
            {
                var newEmployee = addWindow.NewEmployee;
                _dbContext.Employees.Add(newEmployee);
                _dbContext.SaveChanges();
                Employees.Add(newEmployee);
            }
        }

        private void DeleteEmployee()
        {
            if (SelectedEmployee != null)
            {
                // Удаляем связанных детей
                foreach (var child in SelectedEmployee.Children.ToList())
                {
                    _dbContext.Children.Remove(child);
                }

                _dbContext.Employees.Remove(SelectedEmployee);
                _dbContext.SaveChanges();
                Employees.Remove(SelectedEmployee);
                SelectedEmployee = null;
            }
        }

        private void EditEmployee()
        {
            if (SelectedEmployee != null)
            {
                // Создаем копию коллекции детей для редактирования
                var childrenCopy = new ObservableCollection<Child>(
                    SelectedEmployee.Children.Select(c => new Child
                    {
                        FullName = c.FullName,
                        DateOfBirth = c.DateOfBirth,
                        EmployeeId = c.EmployeeId
                    }));

                var employeeCopy = new Employee
                {
                    EmployeeId = SelectedEmployee.EmployeeId,
                    LastName = SelectedEmployee.LastName,
                    FirstName = SelectedEmployee.FirstName,
                    MiddleName = SelectedEmployee.MiddleName,
                    Gender = SelectedEmployee.Gender,
                    BirthDate = SelectedEmployee.BirthDate,
                    AppointmentDate = SelectedEmployee.AppointmentDate,
                    Salary = SelectedEmployee.Salary,
                    PositionId = SelectedEmployee.PositionId,
                    Children = childrenCopy
                };

                var editWindow = new EditEmployee(employeeCopy, Positions);
                if (editWindow.ShowDialog() == true)
                {
                    // Обновляем оригинального сотрудника
                    SelectedEmployee.LastName = employeeCopy.LastName;
                    SelectedEmployee.FirstName = employeeCopy.FirstName;
                    SelectedEmployee.MiddleName = employeeCopy.MiddleName;
                    SelectedEmployee.Gender = employeeCopy.Gender;
                    SelectedEmployee.BirthDate = employeeCopy.BirthDate;
                    SelectedEmployee.AppointmentDate = employeeCopy.AppointmentDate;
                    SelectedEmployee.Salary = employeeCopy.Salary;
                    SelectedEmployee.PositionId = employeeCopy.PositionId;

                    // Обновляем детей
                    SelectedEmployee.Children.Clear();
                    foreach (var child in employeeCopy.Children)
                    {
                        SelectedEmployee.Children.Add(child);
                    }

                    _dbContext.Entry(SelectedEmployee).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }
            }
        }

        private void AddPosition()
        {
            var addWindow = new AddPositionWindow(Departments);
            if (addWindow.ShowDialog() == true)
            {
                _dbContext.Positions.Add(addWindow.NewPosition);
                _dbContext.SaveChanges();

                // Добавляем в локальную коллекцию
                Positions.Add(addWindow.NewPosition);

                // Обновляем навигационные свойства
                addWindow.NewPosition.Department = Departments.FirstOrDefault(d => d.DepartmentId == addWindow.NewPosition.DepartmentId);

                // Уведомляем об изменении коллекции
                OnPropertyChanged(nameof(Positions));
            }
        }

        private void DeletePosition()
        {
            if (SelectedPosition != null)
            {
                _dbContext.Positions.Remove(SelectedPosition);
                _dbContext.SaveChanges();
                Positions.Remove(SelectedPosition);
                SelectedPosition = null;
                OnPropertyChanged(nameof(Positions));
            }
        }

        private void EditPosition()
        {
            if (SelectedPosition != null)
            {
                var editWindow = new EditPosition(SelectedPosition, Departments);
                if (editWindow.ShowDialog() == true)
                {
                    // Обновляем в БД
                    _dbContext.Entry(SelectedPosition).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    // Обновляем навигационные свойства
                    SelectedPosition.Department = Departments.FirstOrDefault(d => d.DepartmentId == SelectedPosition.DepartmentId);

                    // Уведомляем об изменениях
                    OnPropertyChanged(nameof(SelectedPosition));
                    OnPropertyChanged(nameof(Positions));
                }
            }
        }

        private void AddDepartment()
        {
            var addWindow = new AddDepartmentWindow();
            if (addWindow.ShowDialog() == true)
            {
                _dbContext.Departments.Add(addWindow.NewDepartment);
                _dbContext.SaveChanges();

                // Добавляем в локальную коллекцию
                Departments.Add(addWindow.NewDepartment);

                // Уведомляем об изменении коллекции
                OnPropertyChanged(nameof(Departments));
            }
        }


        private void DeleteDepartment()
        {
            if (SelectedDepartment != null)
            {
                // Удаляем связанные позиции
                var relatedPositions = Positions.Where(p => p.DepartmentId == SelectedDepartment.DepartmentId).ToList();
                foreach (var position in relatedPositions)
                {
                    _dbContext.Positions.Remove(position);
                }

                _dbContext.Departments.Remove(SelectedDepartment);
                _dbContext.SaveChanges();

                // Удаляем из локальных коллекций
                foreach (var position in relatedPositions)
                {
                    Positions.Remove(position);
                }
                Departments.Remove(SelectedDepartment);

                SelectedDepartment = null;

                // Уведомляем об изменениях
                OnPropertyChanged(nameof(Departments));
                OnPropertyChanged(nameof(Positions));
            }
        }

        private void EditDepartment()
        {
            if (SelectedDepartment != null)
            {
                var editWindow = new EditDepartment(SelectedDepartment);
                if (editWindow.ShowDialog() == true)
                {
                    // Обновляем в БД
                    _dbContext.Entry(SelectedDepartment).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    // Уведомляем об изменениях
                    OnPropertyChanged(nameof(SelectedDepartment));
                    OnPropertyChanged(nameof(Departments));
                }
            }
        }

        private void AddStaffing()
        {
            var addWindow = new AddStaffingWindow(Departments, Positions);
            if (addWindow.ShowDialog() == true)
            {
                _dbContext.Staffing.Add(addWindow.NewStaffing);
                _dbContext.SaveChanges();

                // Добавляем в локальную коллекцию
                Staffing.Add(addWindow.NewStaffing);

                // Обновляем навигационные свойства
                addWindow.NewStaffing.Department = Departments.FirstOrDefault(d => d.DepartmentId == addWindow.NewStaffing.DepartmentId);
                addWindow.NewStaffing.Position = Positions.FirstOrDefault(p => p.PositionId == addWindow.NewStaffing.PositionId);

                // Уведомляем об изменении коллекции
                OnPropertyChanged(nameof(Staffing));
            }
        }

        private void DeleteStaffing()
        {
            if (SelectedStaffing != null)
            {
                _dbContext.Staffing.Remove(SelectedStaffing);
                _dbContext.SaveChanges();
                Staffing.Remove(SelectedStaffing);
                SelectedStaffing = null;
                OnPropertyChanged(nameof(Staffing));
            }
        }

        private void EditStaffing()
        {
            if (SelectedStaffing != null)
            {
                var editWindow = new EditStaffing(SelectedStaffing, Departments, Positions);
                if (editWindow.ShowDialog() == true)
                {
                    // Обновляем в БД
                    _dbContext.Entry(SelectedStaffing).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    // Обновляем навигационные свойства
                    SelectedStaffing.Department = Departments.FirstOrDefault(d => d.DepartmentId == SelectedStaffing.DepartmentId);
                    SelectedStaffing.Position = Positions.FirstOrDefault(p => p.PositionId == SelectedStaffing.PositionId);

                    // Уведомляем об изменениях
                    OnPropertyChanged(nameof(SelectedStaffing));
                    OnPropertyChanged(nameof(Staffing));
                }
            }
        }

        private void AddPayment()
        {
            var addWindow = new AddPaymentWindow(Positions);
            if (addWindow.ShowDialog() == true)
            {
                _dbContext.AdditionalPayments.Add(addWindow.NewPayment);
                _dbContext.SaveChanges();
                AdditionalPayments.Add(addWindow.NewPayment);

                // Обновляем навигационные свойства
                addWindow.NewPayment.Position = Positions.FirstOrDefault(p => p.PositionId == addWindow.NewPayment.PositionId);
            }
        }

        private void DeletePayment()
        {
            if (SelectedPayment != null)
            {
                var id = SelectedPayment.PaymentId;
                _dbContext.AdditionalPayments.Remove(SelectedPayment);
                _dbContext.SaveChanges();

                // Перезагружаем данные
                AdditionalPayments.Clear();
                AdditionalPayments.AddRange(_dbContext.AdditionalPayments.ToList());

                SelectedPayment = null;
            }
        }

        private void EditPayment()
        {
            if (SelectedPayment != null)
            {
                var editWindow = new EditPayment(SelectedPayment, Positions);
                if (editWindow.ShowDialog() == true)
                {
                    _dbContext.Entry(SelectedPayment).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    // Обновляем навигационные свойства
                    SelectedPayment.Position = Positions.FirstOrDefault(p => p.PositionId == SelectedPayment.PositionId);
                    OnPropertyChanged(nameof(SelectedPayment));
                }
            }
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

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            return new ObservableCollection<T>(source);
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
    



