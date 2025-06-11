using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PopovaMVVM.Model
{
    public class Position : INotifyPropertyChanged
    {
        private int _positionId;
        private string _title;
        private int _departmentId;

        public int PositionId
        {
            get => _positionId;
            set { _positionId = value; OnPropertyChanged(); }
        }

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }

        public int DepartmentId
        {
            get => _departmentId;
            set { _departmentId = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Department _department;
        public Department Department
        {
            get => _department;
            set
            {
                _department = value;
                OnPropertyChanged();
            }
        }
    }
}
