using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PopovaMVVM.Model
{
    public class Child : INotifyPropertyChanged
    {
        private int _childId;
        private int _employeeId;
        private string _fullName;
        private DateTime _birthDate;

        public int ChildId
        {
            get => _childId;
            set { _childId = value; OnPropertyChanged(); }
        }

        public int EmployeeId
        {
            get => _employeeId;
            set { _employeeId = value; OnPropertyChanged(); }
        }

        public string FullName
        {
            get => _fullName;
            set { _fullName = value; OnPropertyChanged(); }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set { _birthDate = value; OnPropertyChanged(); }
        }

        public int Age => DateTime.Now.Year - BirthDate.Year -
            (DateTime.Now.DayOfYear < BirthDate.DayOfYear ? 1 : 0);

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
