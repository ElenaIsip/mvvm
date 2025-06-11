using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PopovaMVVM.Model
{
    public class Staffing : INotifyPropertyChanged
    {
        private int _staffingId;
        private int _positionId;
        private int _departmentId;
        private int _unitsCount;
        private decimal _salary;

        private Department _department;
        public Department Department
        {
            get => _department;
            set { _department = value; OnPropertyChanged(); }
        }

        private Position _position;
        public Position Position
        {
            get => _position;
            set { _position = value; OnPropertyChanged(); }
        }
        public int StaffingId
        {
            get => _staffingId;
            set { _staffingId = value; OnPropertyChanged(); }
        }

        public int PositionId
        {
            get => _positionId;
            set { _positionId = value; OnPropertyChanged(); }
        }

        public int DepartmentId
        {
            get => _departmentId;
            set { _departmentId = value; OnPropertyChanged(); }
        }

        public int UnitsCount
        {
            get => _unitsCount;
            set { _unitsCount = value; OnPropertyChanged(); }
        }

        public decimal Salary
        {
            get => _salary;
            set { _salary = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
