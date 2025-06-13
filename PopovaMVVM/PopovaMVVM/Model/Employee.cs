using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PopovaMVVM.Model
{
    public class Employee : INotifyPropertyChanged
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        private string _lastName;
        private string _firstName;
        private string _middleName;
        private string _gender;
        private DateTime _birthDate;
        private DateTime _appointmentDate;
        private decimal _salary;
        private int _positionId;
        private ObservableCollection<Child> _children = new ObservableCollection<Child>();

        private Position _position;
        internal object Id;
        internal object DateOfBirth;
        internal object HireDate;

        public Position Position
        {
            get => _position;
            set { _position = value; OnPropertyChanged(); }
        }


        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); }
        }

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); }
        }

        public string MiddleName
        {
            get => _middleName;
            set { _middleName = value; OnPropertyChanged(); }
        }

        public string FullName => $"{LastName} {FirstName} {MiddleName}";

        public string Gender
        {
            get => _gender;
            set { _gender = value; OnPropertyChanged(); }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set { _birthDate = value; OnPropertyChanged(); }
        }

        public DateTime AppointmentDate
        {
            get => _appointmentDate;
            set { _appointmentDate = value; OnPropertyChanged(); }
        }

        public decimal Salary
        {
            get => _salary;
            set { _salary = value; OnPropertyChanged(); }
        }

        public int PositionId
        {
            get => _positionId;
            set { _positionId = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Child> Children
        {
            get => _children;
            set { _children = value; OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
